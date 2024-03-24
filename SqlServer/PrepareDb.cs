using Microsoft.EntityFrameworkCore;
using NCG.HR.Data;
using NCG.HR.Models;
namespace NCG.HR.SqlServer;

public static class PrepareDb
{
    public static void PreparePopulation(IApplicationBuilder app, bool isProduction)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            SeedData(scope.ServiceProvider.GetService<ApplicationDbContext>(), isProduction);
        }
    }

    private static void SeedData(ApplicationDbContext? appDbContext, bool isProduction)
    {
        if (isProduction)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try { appDbContext.Database.Migrate(); }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }
        if (!appDbContext.SystemCodeDetails.Any())
        {
            Console.WriteLine("--> We are seeding data...");
            // Initial SystemCode Settings
            FeedCountryCodes(appDbContext);
            FeedCityCodes(appDbContext);
            FeedSystemSettings(appDbContext);
            FeedDepartments(appDbContext);
            FeedLeaveTypes(appDbContext);
            FeedBanks(appDbContext);
            appDbContext.SaveChanges();
        }
        else Console.WriteLine("--> We already have data");
    }

    private static void FeedBanks(ApplicationDbContext appDbContext)
    {
        appDbContext.Banks.AddRange(
            new Bank { Code = "PBC", Name = "中国人民银行", Description = "People's Bank of China)" },
            new Bank { Code = "CCB", Name = "中国建设银行", Description = "China Construction Bank)" },
            new Bank { Code = "ABC", Name = "中国农业银行", Description = "Agricultural Bank of China)" },
            new Bank { Code = "ICBC", Name = "中国工商银行", Description = "Industrial and Commercial Bank of China)" },
            new Bank { Code = "BOC", Name = "中国银行", Description = "Bank of China)" },
            new Bank { Code = "BCM", Name = "交通银行", Description = "Bank of Communications)" },
            new Bank { Code = "CMB", Name = "招商银行", Description = "China Merchants Bank)" },
            new Bank { Code = "CMBC", Name = "中国民生银行", Description = "China Minsheng Banking Co., Ltd)" },
            new Bank { Code = "CIB", Name = "兴业银行", Description = "Industrial Bank Co., Ltd)" },
            new Bank { Code = "BOB", Name = "北京银行", Description = "Bank of Beijing)" },
            new Bank { Code = "CEB", Name = "中国光大银行", Description = "China Everbright Bank)" },
            new Bank { Code = "China CITIC Bank", Name = "中信银行", Description = "" },
            new Bank { Code = "GDB", Name = "广东发展银行", Description = "Guangdong Development Bank)" },
            new Bank { Code = "SPDB/SPDBank", Name = "上海浦东发展银行", Description = "Shanghai Pudong Development Bank)" },
            new Bank { Code = "SDB", Name = "深圳发展银行", Description = "Shenzhen Development Bank)" },
            new Bank { Code = "HXB", Name = "华夏银行", Description = "Huaxia Bank)" },
            new Bank { Code = "CDB", Name = "国家开发银行", Description = "China Development Bank)" },
            new Bank { Code = "The Export-Import Bank of China", Name = "中国进出口银行", Description = "" },
            new Bank { Code = "ADBC", Name = "中国农业发展银行", Description = "Agricultural Development Bank of China)。" });
    }

    private static void FeedLeaveTypes(ApplicationDbContext appDbContext)
    {
        appDbContext.LeaveTypes.AddRange(
            new LeaveType { Code = "LT-000001", Name = "病假", Description = "病假" },
            new LeaveType { Code = "LT-000002", Name = "事假", Description = "事假" },
            new LeaveType { Code = "LT-000003", Name = "婚假", Description = "婚假" },
            new LeaveType { Code = "LT-000004", Name = "产假", Description = "产假" },
            new LeaveType { Code = "LT-000005", Name = "年假", Description = "年假" },
            new LeaveType { Code = "LT-000006", Name = "调休", Description = "调休" },
            new LeaveType { Code = "LT-000007", Name = "外出", Description = "外出" });
    }

    private static void FeedDepartments(ApplicationDbContext appDbContext)
    {
        appDbContext.Departments.AddRange(
            new Department { Name = "总务科", Code = "DC-000001" },
            new Department { Name = "计免科", Code = "DC-000002" },
            new Department { Name = "中医科", Code = "DC-000003" },
            new Department { Name = "办公室", Code = "DC-000004" },
            new Department { Name = "科教科", Code = "DC-000005" },
            new Department { Name = "妇保科", Code = "DC-000006" },
            new Department { Name = "全科", Code = "DC-000007" },
            new Department { Name = "医保办", Code = "DC-000008" },
            new Department { Name = "妇科", Code = "DC-000009" },
            new Department { Name = "儿保科", Code = "DC-000010" },
            new Department { Name = "儿科", Code = "DC-000011" },
            new Department { Name = "药剂科", Code = "DC-000012" },
            new Department { Name = "中心主任", Code = "DC-000013" },
            new Department { Name = "检验科", Code = "DC-000014" },
            new Department { Name = "公卫科", Code = "DC-000015" },
            new Department { Name = "财务科", Code = "DC-000016" },
            new Department { Name = "住院部", Code = "DC-000017" },
            new Department { Name = "体检科", Code = "DC-000018" },
            new Department { Name = "病区护士", Code = "DC-000019" },
            new Department { Name = "影像科", Code = "DC-000020" },
            new Department { Name = "口腔科", Code = "DC-000021" },
            new Department { Name = "门诊部", Code = "DC-000022" },
            new Department { Name = "护理部", Code = "DC-000023" },
            new Department { Name = "医务科", Code = "DC-000024" },
            new Department { Name = "副主任", Code = "DC-000025" },
            new Department { Name = "运管科", Code = "DC-000026" },
            new Department { Name = "皮肤科", Code = "DC-000027" },
            new Department { Name = "麻醉科", Code = "DC-000028" },
            new Department { Name = "信息科", Code = "DC-000029" },
            new Department { Name = "葛陌村站", Code = "DC-000030" },
            new Department { Name = "B超/院感", Code = "DC-000031" },
            new Department { Name = "双巷村站", Code = "DC-000032" },
            new Department { Name = "病区护士站", Code = "DC-000033" },
            new Department { Name = "保健院", Code = "DC-000034" },
            new Department { Name = "职教中心", Code = "DC-000035" },
            new Department { Name = "西航港", Code = "DC-000036" },
            new Department { Name = "一医院", Code = "DC-000037" },
            new Department { Name = "九江", Code = "DC-000038" },
            new Department { Name = "彭镇", Code = "DC-000039" },
            new Department { Name = "金桥", Code = "DC-000040" });
    }

    private static void FeedCityCodes(ApplicationDbContext appDbContext)
    {
        var country = appDbContext.Countries.FirstOrDefault();
        appDbContext.Cities.Add(new City { Name = "四川省成都市", Code = "CC-SC-CD", CountryId = country.Id });
    }

    private static void FeedCountryCodes(ApplicationDbContext appDbContext)
    {
        appDbContext.Countries.Add(new Country { Code = "NC-CN", Name = "中华人民共和国" });
        appDbContext.SaveChanges();
    }

    private static void FeedSystemSettings(ApplicationDbContext appDbContext)
    {
        appDbContext.SystemCodeDetails.AddRange(
            new SystemCodeDetail { Description = "中医士", Code = "SC-PQ-000001" },
            new SystemCodeDetail { Description = "主管护师", Code = "SC-PQ-000002" },
            new SystemCodeDetail { Description = "中医师", Code = "SC-PQ-000003" },
            new SystemCodeDetail { Description = "主治中医师", Code = "SC-PQ-000004" },
            new SystemCodeDetail { Description = "主治医师", Code = "SC-PQ-000005" },
            new SystemCodeDetail { Description = "副主任医师", Code = "SC-PQ-000006" },
            new SystemCodeDetail { Description = "医士", Code = "SC-PQ-000007" },
            new SystemCodeDetail { Description = "医师", Code = "SC-PQ-000008" },
            new SystemCodeDetail { Description = "药剂师", Code = "SC-PQ-000009" },
            new SystemCodeDetail { Description = "护师", Code = "SC-PQ-000010" },
            new SystemCodeDetail { Description = "检验师", Code = "SC-PQ-000011" },
            new SystemCodeDetail { Description = "药剂士", Code = "SC-PQ-000012" },
            new SystemCodeDetail { Description = "基层副主任医师", Code = "SC-PQ-000013" },
            new SystemCodeDetail { Description = "主管中药师", Code = "SC-PQ-000014" },
            new SystemCodeDetail { Description = "副主任护师", Code = "SC-PQ-000015" },
            new SystemCodeDetail { Description = "心理咨询师", Code = "SC-PQ-000016" },
            new SystemCodeDetail { Description = "基层副主任中医师", Code = "SC-PQ-000017" },
            new SystemCodeDetail { Description = "主管技师", Code = "SC-PQ-000018" },
            new SystemCodeDetail { Description = "基层主治中医师", Code = "SC-PQ-000019" },
            new SystemCodeDetail { Description = "主管药剂师", Code = "SC-PQ-000020" },
            new SystemCodeDetail { Description = "主管检验师", Code = "SC-PQ-000021" },
            new SystemCodeDetail { Description = "护士", Code = "SC-PQ-000022" },
            new SystemCodeDetail { Description = "基层主管护师", Code = "SC-PQ-000023" },
            new SystemCodeDetail { Description = "基层主治医师", Code = "SC-PQ-000024" },
            new SystemCodeDetail { Description = "康复治疗师", Code = "SC-PQ-000025" },
            new SystemCodeDetail { Description = "检验士", Code = "SC-PQ-000026" },
            new SystemCodeDetail { Description = "助理医师", Code = "SC-PQ-000027" },
            new SystemCodeDetail { Description = "主任中药师", Code = "SC-PQ-000028" },
            new SystemCodeDetail { Description = "中医士", Code = "SC-RP-000001" },
            new SystemCodeDetail { Description = "主管护师", Code = "SC-RP-000002" },
            new SystemCodeDetail { Description = "中医师", Code = "SC-RP-000003" },
            new SystemCodeDetail { Description = "主治中医师", Code = "SC-RP-000004" },
            new SystemCodeDetail { Description = "主治医师", Code = "SC-RP-000005" },
            new SystemCodeDetail { Description = "副主治医师", Code = "SC-RP-000006" },
            new SystemCodeDetail { Description = "副主任医师", Code = "SC-RP-000007" },
            new SystemCodeDetail { Description = "医士", Code = "SC-RP-000008" },
            new SystemCodeDetail { Description = "医师", Code = "SC-RP-000009" },
            new SystemCodeDetail { Description = "药剂师", Code = "SC-RP-000010" },
            new SystemCodeDetail { Description = "护师", Code = "SC-RP-000011" },
            new SystemCodeDetail { Description = "检验师", Code = "SC-RP-000012" },
            new SystemCodeDetail { Description = "药剂士", Code = "SC-RP-000013" },
            new SystemCodeDetail { Description = "主管中药师", Code = "SC-RP-000014" },
            new SystemCodeDetail { Description = "副主任护师", Code = "SC-RP-000015" },
            new SystemCodeDetail { Description = "心理咨询师", Code = "SC-RP-000016" },
            new SystemCodeDetail { Description = "基层副主任中医师", Code = "SC-RP-000017" },
            new SystemCodeDetail { Description = "影像技师", Code = "SC-RP-000018" },
            new SystemCodeDetail { Description = "主管检验师", Code = "SC-RP-000019" },
            new SystemCodeDetail { Description = "护士", Code = "SC-RP-000020" },
            new SystemCodeDetail { Description = "康复治疗师", Code = "SC-RP-000021" },
            new SystemCodeDetail { Description = "助理医师", Code = "SC-RP-000022" },
            new SystemCodeDetail { Description = "副主任中药师", Code = "SC-RP-000023" },
            new SystemCodeDetail { Description = "副主任药师", Code = "SC-RP-000024" },
            new SystemCodeDetail { Description = "执业助理医师", Code = "SC-QA-000001" },
            new SystemCodeDetail { Description = "执业护士", Code = "SC-QA-000002" },
            new SystemCodeDetail { Description = "执业医师", Code = "SC-QA-000003" },
            new SystemCodeDetail { Description = "药剂师", Code = "SC-QA-000004" },
            new SystemCodeDetail { Description = "检验师", Code = "SC-QA-000005" },
            new SystemCodeDetail { Description = "药剂士", Code = "SC-QA-000006" },
            new SystemCodeDetail { Description = "执业药师", Code = "SC-QA-000007" },
            new SystemCodeDetail { Description = "心理咨询师", Code = "SC-QA-000008" },
            new SystemCodeDetail { Description = "中级技师", Code = "SC-QA-000009" },
            new SystemCodeDetail { Description = "检验师（师）", Code = "SC-QA-000010" },
            new SystemCodeDetail { Description = "主管药剂师", Code = "SC-QA-000011" },
            new SystemCodeDetail { Description = "康复技师", Code = "SC-QA-000012" },
            new SystemCodeDetail { Description = "中医", Code = "SC-PC-000001" },
            new SystemCodeDetail { Description = "护理", Code = "SC-PC-000002" },
            new SystemCodeDetail { Description = "临床", Code = "SC-PC-000003" },
            new SystemCodeDetail { Description = "药学", Code = "SC-PC-000004" },
            new SystemCodeDetail { Description = "检验", Code = "SC-PC-000005" },
            new SystemCodeDetail { Description = "心理学", Code = "SC-PC-000006" },
            new SystemCodeDetail { Description = "口腔", Code = "SC-PC-000007" },
            new SystemCodeDetail { Description = "影像", Code = "SC-PC-000008" },
            new SystemCodeDetail { Description = "公卫", Code = "SC-PC-000009" },
            new SystemCodeDetail { Description = "护理 ", Code = "SC-PC-000010" },
            new SystemCodeDetail { Description = "技师", Code = "SC-PC-000011" },
            new SystemCodeDetail { Description = "初级", Code = "SC-JT-000001" },
            new SystemCodeDetail { Description = "中级", Code = "SC-JT-000002" },
            new SystemCodeDetail { Description = "副高", Code = "SC-JT-000003" },
            new SystemCodeDetail { Description = "正高", Code = "SC-JT-000004" },
            new SystemCodeDetail { Description = "中层", Code = "SC-LP-000001" },
            new SystemCodeDetail { Description = "四川省成都卫生学校", Code = "SC-GS-000001" },
            new SystemCodeDetail { Description = "成都中医药大学", Code = "SC-GS-000002" },
            new SystemCodeDetail { Description = "泸州医学院", Code = "SC-GS-000003" },
            new SystemCodeDetail { Description = "成都市中医药大学", Code = "SC-GS-000004" },
            new SystemCodeDetail { Description = "成都电大", Code = "SC-GS-000005" },
            new SystemCodeDetail { Description = "四川省卫生干部学院", Code = "SC-GS-000006" },
            new SystemCodeDetail { Description = "国家开放大学", Code = "SC-GS-000007" },
            new SystemCodeDetail { Description = "成都医学院", Code = "SC-GS-000008" },
            new SystemCodeDetail { Description = "四川大学", Code = "SC-GS-000009" },
            new SystemCodeDetail { Description = "成都卫校", Code = "SC-GS-000010" },
            new SystemCodeDetail { Description = "西安交通大学", Code = "SC-GS-000011" },
            new SystemCodeDetail { Description = "川北医学院", Code = "SC-GS-000012" },
            new SystemCodeDetail { Description = "中国医科大学", Code = "SC-GS-000013" },
            new SystemCodeDetail { Description = "乐山师范学院", Code = "SC-GS-000014" },
            new SystemCodeDetail { Description = "四川中医药高等专科学校", Code = "SC-GS-000015" },
            new SystemCodeDetail { Description = "河北医科大学", Code = "SC-GS-000016" },
            new SystemCodeDetail { Description = "湖北省长江大学", Code = "SC-GS-000017" },
            new SystemCodeDetail { Description = "河北联合大学", Code = "SC-GS-000018" },
            new SystemCodeDetail { Description = "四川大学青羊校区", Code = "SC-GS-000019" },
            new SystemCodeDetail { Description = "山西医科大学", Code = "SC-GS-000020" },
            new SystemCodeDetail { Description = "西南医科大学", Code = "SC-GS-000021" },
            new SystemCodeDetail { Description = "山东力明科技职业学院", Code = "SC-GS-000022" },
            new SystemCodeDetail { Description = "遵义医学院", Code = "SC-GS-000023" },
            new SystemCodeDetail { Description = "成都体育学院", Code = "SC-GS-000024" },
            new SystemCodeDetail { Description = "佳木斯大学", Code = "SC-GS-000025" },
            new SystemCodeDetail { Description = "济宁医学院", Code = "SC-GS-000026" },
            new SystemCodeDetail { Description = "大连医科大学", Code = "SC-GS-000027" },
            new SystemCodeDetail { Description = "蚌埠医学院", Code = "SC-GS-000028" },
            new SystemCodeDetail { Description = "泰山医学院", Code = "SC-GS-000029" },
            new SystemCodeDetail { Description = "中南大学", Code = "SC-GS-000030" },
            new SystemCodeDetail { Description = "四川农业大学", Code = "SC-GS-000031" },
            new SystemCodeDetail { Description = "广元职工医学院", Code = "SC-GS-000032" },
            new SystemCodeDetail { Description = "成都中医学院", Code = "SC-GS-000033" },
            new SystemCodeDetail { Description = "成都中医药大学附属针灸学院(国家开放大学)", Code = "SC-GS-000034" },
            new SystemCodeDetail { Description = "贵洲省贵阳市贵医附院", Code = "SC-GS-000035" },
            new SystemCodeDetail { Description = "中央广播电视大学", Code = "SC-GS-000036" },
            new SystemCodeDetail { Description = "成都学院", Code = "SC-GS-000037" },
            new SystemCodeDetail { Description = "雅安职业技术学院", Code = "SC-GS-000038" },
            new SystemCodeDetail { Description = "达州职业技术学院", Code = "SC-GS-000039" },
            new SystemCodeDetail { Description = "四川广播电视大学", Code = "SC-GS-000040" },
            new SystemCodeDetail { Description = "四川省人民医院护理学校", Code = "SC-GS-000041" },
            new SystemCodeDetail { Description = "辽宁中医药大学杏林学院", Code = "SC-GS-000042" },
            new SystemCodeDetail { Description = "北京中医药大学", Code = "SC-GS-000043" },
            new SystemCodeDetail { Description = "四川大学附设华西卫生学校", Code = "SC-GS-000044" },
            new SystemCodeDetail { Description = "承德医学院", Code = "SC-GS-000045" },
            new SystemCodeDetail { Description = "南京医科大学", Code = "SC-GS-000046" },
            new SystemCodeDetail { Description = "四川工业科技学院", Code = "SC-GS-000047" },
            new SystemCodeDetail { Description = "长沙民政职业技术学院", Code = "SC-GS-000048" },
            new SystemCodeDetail { Description = "四川护理职业学院", Code = "SC-GS-000049" },
            new SystemCodeDetail { Description = "广西中医药大学赛恩斯新医药学院", Code = "SC-GS-000050" },
            new SystemCodeDetail { Description = "广安职业技术学院", Code = "SC-GS-000051" },
            new SystemCodeDetail { Description = "重庆三峡医药高等专科学校", Code = "SC-GS-000052" },
            new SystemCodeDetail { Description = "四川护理执业学院", Code = "SC-GS-000053" },
            new SystemCodeDetail { Description = "西南财经大学天府学院", Code = "SC-GS-000054" },
            new SystemCodeDetail { Description = "成都文理学院", Code = "SC-GS-000055" },
            new SystemCodeDetail { Description = "重庆医药高等专科学校", Code = "SC-GS-000056" },
            new SystemCodeDetail { Description = "皖南医学院", Code = "SC-GS-000057" },
            new SystemCodeDetail { Description = "中西医结合", Code = "SC-GM-000001" },
            new SystemCodeDetail { Description = "护理", Code = "SC-GM-000002" },
            new SystemCodeDetail { Description = "中医学", Code = "SC-GM-000003" },
            new SystemCodeDetail { Description = "临床医学", Code = "SC-GM-000004" },
            new SystemCodeDetail { Description = "高级护理", Code = "SC-GM-000005" },
            new SystemCodeDetail { Description = "行政管理", Code = "SC-GM-000006" },
            new SystemCodeDetail { Description = "中西医临床医学", Code = "SC-GM-000007" },
            new SystemCodeDetail { Description = "药学", Code = "SC-GM-000008" },
            new SystemCodeDetail { Description = "预防医学", Code = "SC-GM-000009" },
            new SystemCodeDetail { Description = "护理学", Code = "SC-GM-000010" },
            new SystemCodeDetail { Description = "药剂学", Code = "SC-GM-000011" },
            new SystemCodeDetail { Description = "中药学", Code = "SC-GM-000012" },
            new SystemCodeDetail { Description = "教育与心理学", Code = "SC-GM-000013" },
            new SystemCodeDetail { Description = "西医临床", Code = "SC-GM-000014" },
            new SystemCodeDetail { Description = "影像", Code = "SC-GM-000015" },
            new SystemCodeDetail { Description = "口腔医学", Code = "SC-GM-000016" },
            new SystemCodeDetail { Description = "医学影像", Code = "SC-GM-000017" },
            new SystemCodeDetail { Description = "医学检验", Code = "SC-GM-000018" },
            new SystemCodeDetail { Description = "中医学专业（中医骨伤科学方向）", Code = "SC-GM-000019" },
            new SystemCodeDetail { Description = "针灸推拿", Code = "SC-GM-000020" },
            new SystemCodeDetail { Description = "针灸学", Code = "SC-GM-000021" },
            new SystemCodeDetail { Description = "中西医结合临床", Code = "SC-GM-000022" },
            new SystemCodeDetail { Description = "妇产科学", Code = "SC-GM-000023" },
            new SystemCodeDetail { Description = "急诊医学", Code = "SC-GM-000024" },
            new SystemCodeDetail { Description = "医学影像学", Code = "SC-GM-000025" },
            new SystemCodeDetail { Description = "皮肤病与性病学", Code = "SC-GM-000026" },
            new SystemCodeDetail { Description = "麻醉学", Code = "SC-GM-000027" },
            new SystemCodeDetail { Description = "中西医临床", Code = "SC-GM-000028" },
            new SystemCodeDetail { Description = "人力资源管理", Code = "SC-GM-000029" },
            new SystemCodeDetail { Description = "临床", Code = "SC-GM-000030" },
            new SystemCodeDetail { Description = "中医学（中国医科大学药学技术药学）", Code = "SC-GM-000031" },
            new SystemCodeDetail { Description = " 护理学", Code = "SC-GM-000032" },
            new SystemCodeDetail { Description = "针灸推拿学", Code = "SC-GM-000033" },
            new SystemCodeDetail { Description = "康复治疗学", Code = "SC-GM-000034" },
            new SystemCodeDetail { Description = "医学检验技术", Code = "SC-GM-000035" },
            new SystemCodeDetail { Description = "药剂", Code = "SC-GM-000036" },
            new SystemCodeDetail { Description = "中共党员", Code = "SC-PS-000001" },
            new SystemCodeDetail { Description = "团员", Code = "SC-PS-000002" },
            new SystemCodeDetail { Description = "共产党员", Code = "SC-PS-000003" },
            new SystemCodeDetail { Description = "民革", Code = "SC-PS-000004" },
            new SystemCodeDetail { Description = "预备党员", Code = "SC-PS-000005" },
            new SystemCodeDetail { Description = "党员", Code = "SC-PS-000006" },
            new SystemCodeDetail { Description = "干部", Code = "SC-CS-000001" },
            new SystemCodeDetail { Description = "工人", Code = "SC-CS-000002" },
            new SystemCodeDetail { Description = "其他", Code = "SC-CS-000003" },
            new SystemCodeDetail { Description = "中专", Code = "SC-HS-000001" },
            new SystemCodeDetail { Description = "大专(在职）", Code = "SC-HS-000002" },
            new SystemCodeDetail { Description = "本科(在职）", Code = "SC-HS-000003" },
            new SystemCodeDetail { Description = "大专", Code = "SC-HS-000004" },
            new SystemCodeDetail { Description = "本科", Code = "SC-HS-000005" },
            new SystemCodeDetail { Description = "硕士", Code = "SC-HS-000006" },
            new SystemCodeDetail { Description = "研究生（在职）", Code = "SC-HS-000007" },
            new SystemCodeDetail { Description = "研究生（在职）", Code = "SC-HS-000007" },
            new SystemCodeDetail { Description = "正式-离职", Code = "SC-AS-000002" },
            new SystemCodeDetail { Description = "正式-内退", Code = "SC-AS-000003" },
            new SystemCodeDetail { Description = "正式-退休", Code = "SC-AS-000004" },
            new SystemCodeDetail { Description = "正式-在职", Code = "SC-AS-000001" },
            new SystemCodeDetail { Description = "正式-外派", Code = "SC-AS-000005" },
            new SystemCodeDetail { Description = "正式-挂职", Code = "SC-AS-000006" },
            new SystemCodeDetail { Description = "临时-在职", Code = "SC-AS-000007" },
            new SystemCodeDetail { Description = "临时-离职", Code = "SC-AS-000008" },
            new SystemCodeDetail { Description = "临时-借调", Code = "SC-AS-000009" },
            new SystemCodeDetail { Description = "临时-外派", Code = "SC-AS-000010" });
    }
}