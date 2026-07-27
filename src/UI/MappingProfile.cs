using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
// using HR_Management_Application.Dtos;
// using HR_Management_Application.Dtos.LeaveType;
#region IMPORTANT
/* ===========================IMPORTANT===========================
مشکل دقیقاً همینه که تو خودِ خطای exception هم مشخصه:

علت خطا

تو MappingProfile.cs این خط رو داری:

csharp
CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();

و بالای فایل using HR_Management_Application.Dtos; هست. یعنی این map برای کلاس HR_Management_Application.Dtos.LeaveTypeDto تعریف شده.

اما تو LeaveTypeService.cs، وقتی _client.LeaveTypeAllAsync() رو صدا می‌زنی، این متد یه List<LeaveTypeDto> برمی‌گردونه که از فایل generated شده‌ی NSwag (Client.cs) میاد — یعنی از namespace UI.Services.Base، نه از HR_Management_Application.Dtos.

پس عملاً دو تا کلاس مختلف با اسم یکسان LeaveTypeDto داری:

HR_Management_Application.Dtos.LeaveTypeDto ← این یکی رو تو AutoMapper تعریف کردی
UI.Services.Base.LeaveTypeDto ← این یکی واقعاً استفاده میشه (چون LeaveTypeService هم using UI.Services.Base; داره)

چون این دو تا از نظر AutoMapper کاملاً متفاوتن (هرچند هم‌نام)، هیچ map ای براشون تعریف نشده و exception میده:

Missing type map configuration or unsupported mapping.
Mapping types: LeaveTypeDto -> LeaveTypeVM
UI.Services.Base.LeaveTypeDto -> UI.Models.LeaveTypeVM
راه‌حل

تو MappingProfile.cs باید صراحتاً از namespace درست استفاده کنی:

csharp
using System;
using AutoMapper;
using UI.Services.Base;   // <-- namespace درست، به جای HR_Management_Application.Dtos
using UI.Models;

namespace UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
        }
    }
}

نکته مهم: چون LeaveTypeDto و CreateLeaveTypeDto هم تو UI.Services.Base (generated client) و هم تو HR_Management_Application.Dtos وجود دارن، اگه هر دو using رو با هم داشته باشی کامپایلر ارور ambiguity میده. پس باید مطمئن بشی که در MappingProfile.cs فقط using UI.Services.Base; رو داری (و using مربوط به HR_Management_Application.Dtos رو حذف کنی)، چون سرویس‌های UI باید با کلاس‌های generated شده توسط NSwag کار کنن، نه با DTOهای اصلی پروژه‌ی Core.

خلاصه: پروژه UI اصلاً نباید به HR_Management_Application.Dtos رفرنس مستقیم داشته باشه (اون طرف سروره)؛ سمت کلاینت باید فقط با کلاس‌های generated شده در Client.cs (namespace: UI.Services.Base) کار کنه.

گفتی مپر نباید LeaveTypeDto اش رو از src\Core\HR_Management_Application\Dtos\LeaveType\LeaveTypeDto.cs یا لایه بک اند بگیره و از فایل جنریت شده توسط NSAWQ از API CONTROLLER بگیره. این بخاطر جدا کردن وابستگی توی معمیاری تمیز هست ؟ چون تا جایی که یاد گرفتم NSAWQ میاد لایه HTTP رو خودش برامون میسازه تا برنامه ui بتونه از این طریق با api یا همون بک اند حرف بزنه

تأیید کرد مفاهیم معماری تمیز و جدایی وابستگی
تأیید کرد مفاهیم معماری تمیز و جدایی وابستگی

دقیقاً، درست فهمیدی. بذار دقیق‌تر توضیح بدم:

چرا این جدایی مهمه

تو Clean Architecture (که این پروژه ظاهراً داره ازش پیروی می‌کنه)، لایه‌ها اینجورین:

Core (Domain + Application) → Api (Controllers) → UI (Blazor/MVC Client)

پروژه‌ی UI نباید مستقیماً به HR_Management_Application رفرنس داشته باشه. چرا؟ چون:

UI یه کلاینت جداست، نه بخشی از بک‌اند. فرض کن UI رو روی یه سرور دیگه دیپلوی کنی، یا حتی یه اپ موبایل/دسکتاپ دیگه بسازی که با همون API حرف بزنه. اون اصلاً دسترسی به کد Core رو نداره و نباید داشته باشه.
قرارداد ارتباطی UI با بک‌اند، خودِ HTTP API هست، نه کلاس‌های #C مشترک. یعنی UI فقط باید بدونه که اگه GET به /api/LeaveType بزنه، یه JSON با این شکل برمی‌گرده. اینکه پشت صحنه سرور از چه کلاس دامنه‌ای استفاده می‌کنه اصلاً نباید براش مهم باشه.
نقش NSwag اینجا

دقیقاً همونیه که گفتی — NSwag میاد Swagger/OpenAPI spec سرور رو می‌خونه و یه لایه‌ی HTTP Client خودکار (همون Client.cs) برات می‌سازه که:

کلاس‌های DTO رو از روی خروجی API (یعنی از روی JSON schema) دوباره تولید می‌کنه، نه با ارجاع مستقیم به کلاس‌های سمت سرور
متدهای async مثل LeaveTypeAllAsync(), LeaveTypePOSTAsync() و... رو می‌سازه که خودشون serialize/deserialize و HTTP call رو هندل می‌کنن

پس UI.Services.Base.LeaveTypeDto یه کپی مستقل و auto-generated از شکل داده‌ایه که API واقعاً برمی‌گردونه — حتی اگه الان دقیقاً همون property هارو داشته باشه که HR_Management_Application.Dtos.LeaveTypeDto داره.

فایده‌ی عملی این جدایی
اگه فردا لایه‌ی Core تغییر کنه (مثلاً یه property جدید داخلی به DTO اضافه بشه که قرار نیست expose بشه)، UI بهم نمی‌ریزه، چون UI به Swagger spec وابسته‌ست نه به کد داخلی.
می‌تونی UI رو کاملاً جدا build/deploy/version کنی.
Coupling بین لایه‌ها کم میشه — یکی از اصول اصلی Clean Architecture همینه: وابستگی‌ها باید به سمت مرکز (Domain) باشن، نه برعکس، و لایه‌های بیرونی (مثل UI) نباید به جزئیات پیاده‌سازی لایه‌های دیگه‌ی بیرونی (مثل یه UI دیگه یا حتی Infrastructure) گره بخورن.

پس جمع‌بندی: بله، این کار دقیقاً برای رعایت جداسازی (separation of concerns) و loose coupling در معماریه، و راه‌حل درستش همینه که فقط با کلاس generated شده توسط NSwag (UI.Services.Base.LeaveTypeDto) کار کنی و رفرنس مستقیم پروژه UI به HR_Management_Application رو اصلاً حذف کنی.


------------------------------------------------------------------------------
لان یعنی هر ui دیگه ای که بخواد با api یا همون بک اند صحبت بکنه باید از همین فایل src\UI\Services\Base\ServiceClient.cs با محتوای استفاده بکنه ؟ یا باید متناسب با زبان و تکنولوژی خودش دوباره یه فایل http برای خودش از روی api controller و swagger با یه ابزار مثل همین nsawg http اش رو تولید کنه ؟


تحلیل کرد آیا یک کلاینت NSwag برای همه UI ها قابل استفاده است
تحلیل کرد آیا یک کلاینت NSwag برای همه UI ها قابل استفاده است
نه، هر UI دیگه نباید از همین فایل C# استفاده کنه — این نکته‌ی خیلی مهمیه که بهتره روشن بشه.

چرا این فایل قابل استفاده‌ی مجدد بین تکنولوژی‌های مختلف نیست
فایل ServiceClient.cs (همون Client.cs که NSwag تولید کرده) یه فایل #C خام هست که:

به Newtonsoft.Json, System.Net.Http.HttpClient و کلاً runtime دات‌نت وابسته‌ست
کامپایل میشه به IL و فقط داخل یه پروژه‌ی .NET قابل استفاده‌ست
پس اگه یه اپ دیگه با یه تکنولوژی متفاوت (مثلاً React، Flutter، Angular، یا حتی یه سرویس Python) بخواد با همون بک‌اند صحبت کنه، نمی‌تونه این فایل رو import یا reference کنه. این فایل صرفاً مخصوص کلاینت‌های #C/.NET هست.

پس واقعاً چه اتفاقی باید بیفته؟
خودِ منبع حقیقت (source of truth) که بین همه‌ی کلاینت‌ها مشترکه، Swagger/OpenAPI spec هست — یعنی همون فایلی که API بک‌اند exposه می‌کنه (معمولاً یه آدرس مثل /swagger/v1/swagger.json).

هر کلاینت، صرف‌نظر از تکنولوژیش، باید:

یه ابزار code-generator متناسب با زبان خودش انتخاب کنه که از روی همون Swagger spec کد تولید کنه
اون کد تولیدشده رو داخل پروژه‌ی خودش نگه داره
مثال‌هایی از ابزارها بر اساس تکنولوژی:

تکنولوژی UI	ابزار Generator
C#/.NET (Blazor, WPF, MAUI, ...)	NSwag، یا AutoRest، یا OpenAPI Generator
React/Angular/Vue (TypeScript)	openapi-typescript-codegen، orval، swagger-typescript-api
Flutter/Dart	openapi-generator با generator دارت
Python	openapi-python-client
Java/Kotlin (Android)	OpenAPI Generator با generator جاوا/کاتلین
همه‌ی این ابزارها یه چیز مشترک دارن: ورودی‌شون همون Swagger/OpenAPI JSON از بک‌اندت هست، و خروجی‌شون یه HTTP client بومی به زبان خودشونه.

جمع‌بندی
بک‌اند فقط یک قرارداد عمومی expose می‌کنه: Swagger spec.
هر کلاینت (چه C#، چه JS، چه هر چیز دیگه) این spec رو با ابزار مخصوص زبان خودش مصرف می‌کنه و کد HTTP client خودش رو تولید می‌کنه.
این دقیقاً همون فلسفه‌ی طراحی API-first / contract-first هست — بک‌اند یه قرارداد استاندارد (OpenAPI) میده، و هر مصرف‌کننده مستقل از تکنولوژی می‌تونه باهاش کار کنه، بدون اینکه به کد داخلی هیچ کلاینت دیگه‌ای وابسته باشه.

*/
#endregion

using UI.Models;
using UI.Services.Base;

namespace UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
        }
    }
}