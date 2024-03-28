using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
            FeedDepartments(appDbContext);
            FeedLeaveTypes(appDbContext);
            FeedBanks(appDbContext);
            FeedDesignations(appDbContext);
            FeedSystemCodes(appDbContext);
            FeedSystemSettings(appDbContext);
            FeedGenders(appDbContext);
            FeedEmployees(appDbContext);
        }
        else Console.WriteLine("--> We already have data");
    }

    private static void FeedEmployees(ApplicationDbContext appDbContext)
    {
        #region dbInitial
        var CityCode = appDbContext.Cities;
        var CountryCode = appDbContext.Countries;
        var DepartmentCode = appDbContext.Departments;
        var DesignationCode = appDbContext.Designations;
        var LeaveType = appDbContext.LeaveTypes;
        var Contract = appDbContext.EmployeeContracts;
        var Bank = appDbContext.Banks;
        var SystemCode_Status = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-SA-"));
        var SystemCode_GenderCode = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-GC-"));
        var SystemCode_CauseOfInactivity = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-COI"));
        var SystemCode_ReasonForTermination = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-RFT"));
        var SystemCode_EmploymentTerms = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-ET"));
        var SystemCode_ActiveStatus = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-AS-"));
        var SystemCode_Nation = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-NA-"));
        var SystemCode_PoliticalStatus = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-PS-"));
        var SystemCode_CadreStatus = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-CS-"));
        var SystemCode_HighestEducation = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-HS-"));
        var SystemCode_GraduatedSchool = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-GS-"));
        var SystemCode_GraduatedMajor = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-GM-"));
        var SystemCode_ProfessionalQualification = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-PQ-"));
        var SystemCode_RecruitmentPosition = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-RP-"));
        var SystemCode_Qualification = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-QA-"));
        var SystemCode_ProfessionalCategory = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-PC-"));
        var SystemCode_JobTitle = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-JT-"));
        var SystemCode_leadershipPosition = appDbContext.SystemCodeDetails.Where(r => r.Code.Contains("SC-LP-"));
        #endregion

        var list = GetEmployees();
        var countryId = appDbContext.Countries.FirstOrDefault().Id;
        foreach (var item in list)
        {
            //插入新的City
            if (!string.IsNullOrEmpty(item.CityId))
            {
                if (!CityCode.Any(r => r.Name == item.CityId))
                {
                    var code = "CC-";
                    // 获取最新的code
                    var number = CityCode.OrderByDescending(r => r.Code).FirstOrDefault();
                    if (null == number)
                        code += 1.ToString("D6");
                    else
                    {
                        var index = int.Parse(number.Code.Substring(3)) + 1;
                        code += index.ToString("D6");
                    }
                    var city = new City { CountryId = countryId, Name = item.CityId, Code = code };
                    appDbContext.Cities.Add(city);
                    appDbContext.SaveChanges();
                }
            }
            //插入新的民族
            if (!string.IsNullOrEmpty(item.NationId))
            {
                if (!SystemCode_Nation.Any(r => r.Description == item.NationId))
                {
                    var code = "SC-NA-";
                    var codeId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-NA").Id;

                    // 获取最新的code
                    var number = SystemCode_Nation.OrderByDescending(r => r.Code).FirstOrDefault();
                    if (null == number)
                        code += 1.ToString("D6");
                    else
                    {
                        var index = int.Parse(number.Code.Substring(6)) + 1;
                        code += index.ToString("D6");
                    }
                    var temp = new SystemCodeDetail { Description = item.NationId, Code = code, SystemCodeId = codeId };
                    appDbContext.SystemCodeDetails.Add(temp);
                    appDbContext.SaveChanges();
                }
            }
            //插入新的学校
            if (!string.IsNullOrEmpty(item.GraduatedSchoolId))
            {
                if (!SystemCode_GraduatedSchool.Any(r => r.Description == item.GraduatedSchoolId))
                {
                    var code = "SC-GS-";
                    var codeId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-GS").Id;

                    // 获取最新的code
                    var number = SystemCode_GraduatedSchool.OrderByDescending(r => r.Code).FirstOrDefault();
                    if (null == number)
                        code += 1.ToString("D6");
                    else
                    {
                        var index = int.Parse(number.Code.Substring(6)) + 1;
                        code += index.ToString("D6");
                    }
                    var temp = new SystemCodeDetail { Description = item.GraduatedSchoolId, Code = code, SystemCodeId = codeId };
                    appDbContext.SystemCodeDetails.Add(temp);
                    appDbContext.SaveChanges();
                }
            }
            //插入新的专业
            if (!string.IsNullOrEmpty(item.GraduatedMajorId))
            {
                if (!SystemCode_GraduatedMajor.Any(r => r.Description == item.GraduatedMajorId))
                {
                    var code = "SC-GM-";
                    var codeId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-GM").Id;
                    // 获取最新的code
                    var number = SystemCode_GraduatedMajor.OrderByDescending(r => r.Code).FirstOrDefault();
                    if (null == number)
                        code += 1.ToString("D6");
                    else
                    {
                        var index = int.Parse(number.Code.Substring(6)) + 1;
                        code += index.ToString("D6");
                    }
                    var temp = new SystemCodeDetail { Description = item.GraduatedMajorId, Code = code, SystemCodeId = codeId };
                    appDbContext.SystemCodeDetails.Add(temp);
                    appDbContext.SaveChanges();
                }
            }
        }

        foreach (var item in list)
        {
            var employee = new Employee();

            employee.EmpNo = item.EmpNo;
            employee.CountryId = countryId;
            employee.DepartmentId = string.IsNullOrEmpty(item.DepartmentId) ? null : DepartmentCode.FirstOrDefault(r => r.Name == item.DepartmentId).Id;
            employee.FirstName = item.FirstName.Substring(0, 1);
            employee.MiddleName = item.FirstName.Substring(1);
            employee.GenderId = string.IsNullOrEmpty(item.GenderId) ? null : SystemCode_GenderCode.FirstOrDefault(r => r.Description.Contains(item.GenderId)).Id;
            employee.IdentityNumber = item.IdentityNumber;
            employee.DateOfBirth = Convert.ToDateTime(item.DateOfBirth);
            employee.GraduatedMajorId = string.IsNullOrEmpty(item.GraduatedMajorId) ? null : SystemCode_GraduatedMajor.FirstOrDefault(r => r.Description == item.GraduatedMajorId).Id;
            employee.GraduatedTime = string.IsNullOrEmpty(item.GraduatedTime) ? null : Convert.ToDateTime(item.GraduatedTime);
            employee.StartWorkTime = string.IsNullOrEmpty(item.StartWorkTime) ? null : Convert.ToDateTime(item.StartWorkTime);
            employee.WorkInUnitTime = string.IsNullOrEmpty(item.WorkInUnitTime) ? null : Convert.ToDateTime(item.WorkInUnitTime);
            employee.ProfessionalQualificationId = string.IsNullOrEmpty(item.ProfessionalQualificationId) ? null : SystemCode_ProfessionalQualification.FirstOrDefault(r => r.Description == item.ProfessionalQualificationId).Id;
            employee.GetProfessionalTime = string.IsNullOrEmpty(item.GetProfessionalTime) ? null : Convert.ToDateTime(item.GetProfessionalTime);
            employee.RecruitmentPositionId = string.IsNullOrEmpty(item.RecruitmentPositionId) ? null : SystemCode_RecruitmentPosition.FirstOrDefault(r => r.Description == item.RecruitmentPositionId).Id;
            employee.QualificationId = string.IsNullOrEmpty(item.QualificationId) ? null : SystemCode_Qualification.FirstOrDefault(r => r.Description == item.QualificationId).Id; ;
            employee.ProfessionalCategoryId = string.IsNullOrEmpty(item.ProfessionalCategoryId) ? null : SystemCode_ProfessionalCategory.FirstOrDefault(r => r.Description == item.ProfessionalCategoryId).Id;
            employee.JobTitleId = string.IsNullOrEmpty(item.JobTitleId) ? null : SystemCode_JobTitle.FirstOrDefault(r => r.Description == item.JobTitleId).Id;
            employee.IsGeneral = true;
            employee.NationId = string.IsNullOrEmpty(item.NationId) ? null : SystemCode_Nation.FirstOrDefault(r => r.Description == item.NationId).Id;
            employee.PhoneNumber = item.PhoneNumber;
            employee.Phone = item.PhoneNumber;
            employee.PoliticalStatusId = string.IsNullOrEmpty(item.PoliticalStatusId) ? null : SystemCode_PoliticalStatus.FirstOrDefault(r => r.Description == item.PoliticalStatusId).Id;
            employee.JoinPartyTime = string.IsNullOrEmpty(item.JoinPartyTime) ? null : Convert.ToDateTime(item.JoinPartyTime);
            employee.CadreStatusId = string.IsNullOrEmpty(item.CadreStatusId) ? null : SystemCode_CadreStatus.FirstOrDefault(r => r.Description == item.CadreStatusId).Id;
            employee.HighestEducationId = string.IsNullOrEmpty(item.HighestEducationId) ? null : SystemCode_HighestEducation.FirstOrDefault(r => r.Description == item.HighestEducationId).Id;
            employee.GraduatedSchoolId = string.IsNullOrEmpty(item.GraduatedSchoolId) ? null : SystemCode_GraduatedSchool.FirstOrDefault(r => r.Description == item.GraduatedSchoolId).Id;
            employee.CityId = string.IsNullOrEmpty(item.CityId) ? null : CityCode.FirstOrDefault(r => r.Name == item.CityId).Id;

            appDbContext.Employees.Add(employee);
            Console.WriteLine($"Name is : {employee.FirstName}");
        }
        appDbContext.SaveChanges();
    }


    private static EmployeeImportEntity[] GetEmployees()
    {
        return new[]{
          new EmployeeImportEntity { EmpNo = "EN-000001", DepartmentId = "总务科", FirstName = "陈万军", GenderId = "男", IdentityNumber = "510122196508258311", DateOfBirth = "1965-08-25", CityId = "四川双流", NationId = "汉", PhoneNumber = "13981738383", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "中专", GraduatedSchoolId = "四川省成都卫生学校", GraduatedMajorId = "中西医结合", GraduatedTime = "", StartWorkTime = "1983-06-01", WorkInUnitTime = "1997-04-18", ProfessionalQualificationId = "中医士", GetProfessionalTime = "", RecruitmentPositionId = "中医士", QualificationId = "执业助理医师", ProfessionalCategoryId = "中医", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000002", DepartmentId = "计免科", FirstName = "吴凌艳", GenderId = "女", IdentityNumber = "512924197606051325", DateOfBirth = "1976-06-05", CityId = "四川双流", NationId = "汉", PhoneNumber = "13060055506", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "1995-10-01", WorkInUnitTime = "2000-11-01", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000003", DepartmentId = "中医科", FirstName = "杨树明", GenderId = "男", IdentityNumber = "510122196505164117", DateOfBirth = "1965-05-16", CityId = "四川双流", NationId = "汉", PhoneNumber = "13908203238", PoliticalStatusId = "中共党员", JoinPartyTime = "2008-10-31", CadreStatusId = "干部", HighestEducationId = "大专(在职）", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "中西医结合", GraduatedTime = "", StartWorkTime = "1983-08-01", WorkInUnitTime = "1983-08-01", ProfessionalQualificationId = "中医师", GetProfessionalTime = "", RecruitmentPositionId = "中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000004", DepartmentId = "办公室", FirstName = "廖娜", GenderId = "女", IdentityNumber = "51012219770810002x", DateOfBirth = "1977-08-10", CityId = "四川双流", NationId = "汉", PhoneNumber = "13980674653", PoliticalStatusId = "中共党员", JoinPartyTime = "2001-03-03", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "中医学", GraduatedTime = "", StartWorkTime = "1998-11-13", WorkInUnitTime = "1998-11-13", ProfessionalQualificationId = "主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "主治中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000005", DepartmentId = "科教科", FirstName = "李军", GenderId = "男", IdentityNumber = "510122197708094213", DateOfBirth = "1977-08-09", CityId = "四川双流", NationId = "汉", PhoneNumber = "15828457074", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专(在职）", GraduatedSchoolId = "成都市中医药大学", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "1997-12-09", WorkInUnitTime = "1997-12-09", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000006", DepartmentId = "妇保科", FirstName = "庞宇", GenderId = "女", IdentityNumber = "510122197712035080", DateOfBirth = "1977-12-03", CityId = "四川双流", NationId = "汉", PhoneNumber = "18280066118", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专(在职）", GraduatedSchoolId = "成都电大", GraduatedMajorId = "高级护理", GraduatedTime = "", StartWorkTime = "1996-11-01", WorkInUnitTime = "1996-11-01", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000007", DepartmentId = "全科", FirstName = "潘红艳", GenderId = "女", IdentityNumber = "510122197409273369", DateOfBirth = "1974-09-27", CityId = "四川双流", NationId = "汉", PhoneNumber = "13541112432", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川省卫生干部学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "1996-12-01", WorkInUnitTime = "1996-12-01", ProfessionalQualificationId = "副主任医师", GetProfessionalTime = "", RecruitmentPositionId = "副主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "副高", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000008", DepartmentId = "妇保科", FirstName = "高文芳", GenderId = "女", IdentityNumber = "510122197601051782", DateOfBirth = "1976-01-05", CityId = "四川双流", NationId = "汉", PhoneNumber = "15982899957", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专(在职）", GraduatedSchoolId = "成都电大", GraduatedMajorId = "高级护理", GraduatedTime = "", StartWorkTime = "1996-11-01", WorkInUnitTime = "2004-03-26", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000009", DepartmentId = "医保办", FirstName = "鲜晔", GenderId = "女", IdentityNumber = "510122198010013367", DateOfBirth = "1980-10-01", CityId = "四川双流", NationId = "汉", PhoneNumber = "13628026361", PoliticalStatusId = "中共党员", JoinPartyTime = "2021-07-01", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "国家开放大学", GraduatedMajorId = "行政管理", GraduatedTime = "", StartWorkTime = "1999-09-01", WorkInUnitTime = "2007-03-12", ProfessionalQualificationId = "中医师", GetProfessionalTime = "", RecruitmentPositionId = "中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000010", DepartmentId = "妇保科", FirstName = "谢江慧", GenderId = "女", IdentityNumber = "510105198106191022", DateOfBirth = "1981-06-19", CityId = "四川双流", NationId = "汉", PhoneNumber = "13880569009", PoliticalStatusId = "中共党员", JoinPartyTime = "2007-06-15", CadreStatusId = "干部", HighestEducationId = "大专(在职）", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2000-10-20", WorkInUnitTime = "2000-10-01", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000011", DepartmentId = "妇科", FirstName = "张玲", GenderId = "女", IdentityNumber = "510122198111094944", DateOfBirth = "1981-11-09", CityId = "四川双流", NationId = "汉", PhoneNumber = "18080012119", PoliticalStatusId = "中共党员", JoinPartyTime = "2017-07-01", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2000-10-18", WorkInUnitTime = "2000-10-01", ProfessionalQualificationId = "副主任医师", GetProfessionalTime = "", RecruitmentPositionId = "副主任医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "副高", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000012", DepartmentId = "中医科", FirstName = "罗斌", GenderId = "男", IdentityNumber = "510122198110120012", DateOfBirth = "1981-10-12", CityId = "四川双流", NationId = "汉", PhoneNumber = "18602835635", PoliticalStatusId = "中共党员", JoinPartyTime = "2009-07-01", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "中西医临床医学", GraduatedTime = "", StartWorkTime = "2000-12-16", WorkInUnitTime = "2000-12-16", ProfessionalQualificationId = "主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "主治中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000013", DepartmentId = "儿保科", FirstName = "殷学莉", GenderId = "女", IdentityNumber = "510107197910302182", DateOfBirth = "1979-10-30", CityId = "四川双流", NationId = "汉", PhoneNumber = "13880882809", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2000-10-21", WorkInUnitTime = "2006-06-27", ProfessionalQualificationId = "医士", GetProfessionalTime = "", RecruitmentPositionId = "医士", QualificationId = "执业助理医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000014", DepartmentId = "儿科", FirstName = "熊琴", GenderId = "女", IdentityNumber = "510122198001145368", DateOfBirth = "1980-01-14", CityId = "四川双流", NationId = "汉", PhoneNumber = "13438181933", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2001-12-13", WorkInUnitTime = "2001-12-13", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000015", DepartmentId = "药剂科", FirstName = "陈亮", GenderId = "男", IdentityNumber = "500106198405040894", DateOfBirth = "1984-05-04", CityId = "四川双流", NationId = "藏", PhoneNumber = "13880931766", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "药学", GraduatedTime = "", StartWorkTime = "2002-12-01", WorkInUnitTime = "2002-12-01", ProfessionalQualificationId = "药剂师", GetProfessionalTime = "", RecruitmentPositionId = "药剂师", QualificationId = "药剂师", ProfessionalCategoryId = "药学", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000016", DepartmentId = "中心主任", FirstName = "倪娟", GenderId = "女", IdentityNumber = "510122197010070260", DateOfBirth = "1970-10-07", CityId = "四川双流", NationId = "汉", PhoneNumber = "13982269012", PoliticalStatusId = "中共党员", JoinPartyTime = "2011-10-20", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川大学", GraduatedMajorId = "预防医学", GraduatedTime = "", StartWorkTime = "1989-01-01", WorkInUnitTime = "1996-01-01", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000017", DepartmentId = "检验科", FirstName = "张健", GenderId = "男", IdentityNumber = "510122198208141911", DateOfBirth = "1982-08-14", CityId = "四川双流", NationId = "汉", PhoneNumber = "15908135498", PoliticalStatusId = "中共党员", JoinPartyTime = "2021-09-23", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2003-12-11", WorkInUnitTime = "2003-12-11", ProfessionalQualificationId = "检验师", GetProfessionalTime = "", RecruitmentPositionId = "检验师", QualificationId = "检验师", ProfessionalCategoryId = "检验", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000018", DepartmentId = "妇保科", FirstName = "辜文英", GenderId = "女", IdentityNumber = "513822198312307089", DateOfBirth = "1983-12-30", CityId = "四川双流", NationId = "汉", PhoneNumber = "13488913143", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2003-12-11", WorkInUnitTime = "2003-12-11", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000019", DepartmentId = "公卫科", FirstName = "曹志荣", GenderId = "男", IdentityNumber = "510122198403283376", DateOfBirth = "1984-03-28", CityId = "四川双流", NationId = "汉", PhoneNumber = "18583672669", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专", GraduatedSchoolId = "成都卫校", GraduatedMajorId = "药剂学", GraduatedTime = "", StartWorkTime = "2003-12-11", WorkInUnitTime = "2003-12-11", ProfessionalQualificationId = "药剂士", GetProfessionalTime = "", RecruitmentPositionId = "药剂士", QualificationId = "药剂士", ProfessionalCategoryId = "药学", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000020", DepartmentId = "科教科", FirstName = "刘超英", GenderId = "女", IdentityNumber = "510122198104170048", DateOfBirth = "1981-04-17", CityId = "四川双流", NationId = "汉", PhoneNumber = "13668195436", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "高级护理", GraduatedTime = "", StartWorkTime = "2005-01-01", WorkInUnitTime = "2008-06-25", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000021", DepartmentId = "总务科", FirstName = "周江", GenderId = "男", IdentityNumber = "500231198609230018", DateOfBirth = "1986-09-23", CityId = "四川双流", NationId = "汉", PhoneNumber = "13438877950", PoliticalStatusId = "中共党员", JoinPartyTime = "2020-07-01", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "西安交通大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2006-09-01", WorkInUnitTime = "2006-10-12", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000022", DepartmentId = "财务科", FirstName = "付静", GenderId = "女", IdentityNumber = "510122198610087765", DateOfBirth = "1986-10-08", CityId = "四川双流", NationId = "汉", PhoneNumber = "15982065948", PoliticalStatusId = "中共党员", JoinPartyTime = "2011-09-30", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "高级护理", GraduatedTime = "", StartWorkTime = "2006-09-01", WorkInUnitTime = "2006-10-12", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000023", DepartmentId = "住院部", FirstName = "朱富莲", GenderId = "女", IdentityNumber = "510521198012107602", DateOfBirth = "1980-12-10", CityId = "四川泸州", NationId = "汉", PhoneNumber = "13458563168", PoliticalStatusId = "中共党员", JoinPartyTime = "2005-06-01", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "川北医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2007-09-25", WorkInUnitTime = "2007-09-25", ProfessionalQualificationId = "基层副主任医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "副高", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000024", DepartmentId = "计免科", FirstName = "陈卫", GenderId = "女", IdentityNumber = "510122198902211967", DateOfBirth = "1989-02-21", CityId = "四川双流", NationId = "汉", PhoneNumber = "13880261598", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2007-09-25", WorkInUnitTime = "2007-09-25", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000025", DepartmentId = "药剂科", FirstName = "吴美", GenderId = "女", IdentityNumber = "510122198209290046", DateOfBirth = "1982-09-29", CityId = "四川双流", NationId = "汉", PhoneNumber = "15982011953", PoliticalStatusId = "中共党员", JoinPartyTime = "2003-06-19", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "中药学", GraduatedTime = "", StartWorkTime = "2006-04-01", WorkInUnitTime = "2008-09-08", ProfessionalQualificationId = "主管中药师", GetProfessionalTime = "", RecruitmentPositionId = "主管中药师", QualificationId = "执业药师", ProfessionalCategoryId = "药学", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000026", DepartmentId = "住院部", FirstName = "范敏", GenderId = "女", IdentityNumber = "510122198504245926", DateOfBirth = "1985-04-24", CityId = "四川双流", NationId = "汉", PhoneNumber = "15928406041", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2008-08-01", WorkInUnitTime = "2008-09-08", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000027", DepartmentId = "全科", FirstName = "彭波", GenderId = "男", IdentityNumber = "510122198506273373", DateOfBirth = "1985-06-27", CityId = "四川双流", NationId = "汉", PhoneNumber = "13438175775", PoliticalStatusId = "中共党员", JoinPartyTime = "2005-11-10", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2008-08-01", WorkInUnitTime = "2008-09-08", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000028", DepartmentId = "公卫科", FirstName = "冷小华", GenderId = "女", IdentityNumber = "51010619820309292x", DateOfBirth = "1982-03-09", CityId = "四川双流", NationId = "汉", PhoneNumber = "13551191032", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2000-10-01", WorkInUnitTime = "2008-08-15", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000029", DepartmentId = "公卫科", FirstName = "游海", GenderId = "男", IdentityNumber = "510122198607140094", DateOfBirth = "1986-07-14", CityId = "四川双流", NationId = "汉", PhoneNumber = "13980529620", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "中医学", GraduatedTime = "", StartWorkTime = "2005-01-01", WorkInUnitTime = "2008-08-14", ProfessionalQualificationId = "", GetProfessionalTime = "", RecruitmentPositionId = "", QualificationId = "", ProfessionalCategoryId = "", JobTitleId = "", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000030", DepartmentId = "全科", FirstName = "罗红云", GenderId = "女", IdentityNumber = "510122197011250044", DateOfBirth = "1970-11-25", CityId = "四川双流", NationId = "汉", PhoneNumber = "13689026216", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "高级护理", GraduatedTime = "", StartWorkTime = "1991-08-01", WorkInUnitTime = "2009-04-29", ProfessionalQualificationId = "副主任护师", GetProfessionalTime = "", RecruitmentPositionId = "副主任护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "副高", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000031", DepartmentId = "公卫科", FirstName = "李春梅", GenderId = "女", IdentityNumber = "510122198503096228", DateOfBirth = "1985-03-09", CityId = "四川双流", NationId = "汉", PhoneNumber = "13688198828", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "乐山师范学院", GraduatedMajorId = "教育与心理学", GraduatedTime = "", StartWorkTime = "2008-09-01", WorkInUnitTime = "2009-08-16", ProfessionalQualificationId = "心理咨询师", GetProfessionalTime = "", RecruitmentPositionId = "心理咨询师", QualificationId = "心理咨询师", ProfessionalCategoryId = "心理学", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000032", DepartmentId = "计免科", FirstName = "王玮玲", GenderId = "女", IdentityNumber = "510122198709163369", DateOfBirth = "1987-09-16", CityId = "四川双流", NationId = "汉", PhoneNumber = "13982129884", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专", GraduatedSchoolId = "四川中医药高等专科学校", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2009-08-16", WorkInUnitTime = "2009-08-16", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000033", DepartmentId = "医保办", FirstName = "周瑞", GenderId = "女", IdentityNumber = "650204198406200023", DateOfBirth = "1984-06-20", CityId = "四川双流", NationId = "汉", PhoneNumber = "13551341520", PoliticalStatusId = "中共党员", JoinPartyTime = "2004-12-12", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "西医临床", GraduatedTime = "", StartWorkTime = "2009-08-16", WorkInUnitTime = "2009-08-16", ProfessionalQualificationId = "", GetProfessionalTime = "", RecruitmentPositionId = "", QualificationId = "", ProfessionalCategoryId = "临床", JobTitleId = "", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000034", DepartmentId = "体检科", FirstName = "阙霞", GenderId = "女", IdentityNumber = "510122197908212683", DateOfBirth = "1979-08-21", CityId = "四川双流", NationId = "汉", PhoneNumber = "13550202632", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川省卫生干部学院", GraduatedMajorId = "影像", GraduatedTime = "", StartWorkTime = "2007-10-01", WorkInUnitTime = "2010-09-16", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000035", DepartmentId = "病区护士", FirstName = "张溱", GenderId = "女", IdentityNumber = "513822198510237405", DateOfBirth = "1985-10-23", CityId = "四川双流", NationId = "汉", PhoneNumber = "13699476628", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2008-09-01", WorkInUnitTime = "2010-09-16", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000036", DepartmentId = "儿科", FirstName = "杨丽华", GenderId = "女", IdentityNumber = "511621198606015760", DateOfBirth = "1986-06-01", CityId = "四川广安", NationId = "汉", PhoneNumber = "13880175280", PoliticalStatusId = "中共党员", JoinPartyTime = "2008-11-18", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2010-09-16", WorkInUnitTime = "2010-09-16", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000037", DepartmentId = "儿科", FirstName = "吴昕", GenderId = "女", IdentityNumber = "513325198811220027", DateOfBirth = "1988-11-22", CityId = "四川甘孜", NationId = "藏", PhoneNumber = "18048552206", PoliticalStatusId = "中共党员", JoinPartyTime = "2010-11-12", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2012-07-16", WorkInUnitTime = "2012-07-16", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000038", DepartmentId = "计免科", FirstName = "曾徐瑾", GenderId = "女", IdentityNumber = "510122198812114143", DateOfBirth = "1988-12-11", CityId = "四川双流", NationId = "汉", PhoneNumber = "15198020696", PoliticalStatusId = "中共党员", JoinPartyTime = "2008.8.1", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2010-09-01", WorkInUnitTime = "2012-07-16", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000039", DepartmentId = "影像科", FirstName = "吴萍", GenderId = "女", IdentityNumber = "510182198109212821", DateOfBirth = "1981-09-21", CityId = "四川双流", NationId = "汉", PhoneNumber = "15982851606", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2008-01-01", WorkInUnitTime = "2013-03-01", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000040", DepartmentId = "儿科", FirstName = "黎娟", GenderId = "女", IdentityNumber = "51012219810720002X", DateOfBirth = "1981-07-20", CityId = "四川双流", NationId = "汉", PhoneNumber = "15198101389", PoliticalStatusId = "", JoinPartyTime = "2023.8.30", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "河北医科大学", GraduatedMajorId = "中医学", GraduatedTime = "", StartWorkTime = "2008-01-01", WorkInUnitTime = "2013-03-01", ProfessionalQualificationId = "基层副主任中医师", GetProfessionalTime = "", RecruitmentPositionId = "基层副主任中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "副高", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000041", DepartmentId = "全科", FirstName = "周月琳", GenderId = "女", IdentityNumber = "510122199003235361", DateOfBirth = "1990-03-23", CityId = "四川双流", NationId = "汉", PhoneNumber = "13408646217", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "川北医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2013-08-22", WorkInUnitTime = "2013-08-22", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000042", DepartmentId = "儿科", FirstName = "冯波", GenderId = "男", IdentityNumber = "510122198610270295", DateOfBirth = "1986-10-27", CityId = "四川双流", NationId = "汉", PhoneNumber = "13689058990", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2010-09-01", WorkInUnitTime = "2013-08-22", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000043", DepartmentId = "儿保科", FirstName = "蒋娜", GenderId = "女", IdentityNumber = "51012219860111002X", DateOfBirth = "1986-01-11", CityId = "四川双流", NationId = "汉", PhoneNumber = "15348185208", PoliticalStatusId = "中共党员", JoinPartyTime = "2008-06-13", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "湖北省长江大学", GraduatedMajorId = "西医临床", GraduatedTime = "", StartWorkTime = "2009-09-01", WorkInUnitTime = "2013-08-22", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000044", DepartmentId = "口腔科", FirstName = "杜丽平", GenderId = "女", IdentityNumber = "500242198709243325", DateOfBirth = "1987-09-24", CityId = "重庆酉阳", NationId = "土家族", PhoneNumber = "15208239970", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "河北联合大学", GraduatedMajorId = "口腔医学", GraduatedTime = "", StartWorkTime = "2012-06-01", WorkInUnitTime = "2013-08-22", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "口腔", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000045", DepartmentId = "住院部", FirstName = "陈琳", GenderId = "男", IdentityNumber = "513822198710232430", DateOfBirth = "1987-10-23", CityId = "四川乐山", NationId = "汉", PhoneNumber = "18328555405", PoliticalStatusId = "中共党员", JoinPartyTime = "2011-05-20", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2013-08-22", WorkInUnitTime = "2013-08-22", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000046", DepartmentId = "门诊部", FirstName = "贺伟　", GenderId = "男", IdentityNumber = "513435198009080010", DateOfBirth = "1980-09-08", CityId = "凉山州甘洛", NationId = "汉", PhoneNumber = "13880369098", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "四川大学青羊校区", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2007-12-01", WorkInUnitTime = "2013-08-22", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000047", DepartmentId = "中医科", FirstName = "曾文姣", GenderId = "女", IdentityNumber = "510125198711150021", DateOfBirth = "1987-11-15", CityId = "四川新都", NationId = "回族", PhoneNumber = "13880114210", PoliticalStatusId = "中共党员", JoinPartyTime = "2010-12-16", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "中西医临床医学", GraduatedTime = "", StartWorkTime = "2013-08-22", WorkInUnitTime = "2013-08-22", ProfessionalQualificationId = "主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "主治中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000048", DepartmentId = "护理部", FirstName = "宁婧", GenderId = "女", IdentityNumber = "510122198911142561", DateOfBirth = "1989-11-14", CityId = "四川双流", NationId = "汉", PhoneNumber = "13882151994", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2012-10-01", WorkInUnitTime = "2013-08-22", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000049", DepartmentId = "住院部", FirstName = "唐甜城", GenderId = "女", IdentityNumber = "511023198902279066", DateOfBirth = "1989-02-27", CityId = "四川资阳", NationId = "汉", PhoneNumber = "18280211534", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "山西医科大学", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2014-08-25", WorkInUnitTime = "2014-08-25", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000050", DepartmentId = "影像科", FirstName = "杨秀云", GenderId = "女", IdentityNumber = "511023198905282965", DateOfBirth = "1989-05-28", CityId = "四川资阳", NationId = "汉", PhoneNumber = "18684000382", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "西南医科大学", GraduatedMajorId = "医学影像", GraduatedTime = "", StartWorkTime = "2010-12-01", WorkInUnitTime = "2014-08-25", ProfessionalQualificationId = "主管技师", GetProfessionalTime = "", RecruitmentPositionId = "影像技师", QualificationId = "中级技师", ProfessionalCategoryId = "影像", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000051", DepartmentId = "全科", FirstName = "潘俊", GenderId = "女", IdentityNumber = "511025198810161880", DateOfBirth = "1988-10-16", CityId = "四川资中", NationId = "汉", PhoneNumber = "13408566593", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2010-12-01", WorkInUnitTime = "2014-08-25", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000052", DepartmentId = "妇科", FirstName = "陈婷", GenderId = "女", IdentityNumber = "510122199107273362", DateOfBirth = "1991-07-27", CityId = "四川双流", NationId = "汉", PhoneNumber = "18011479837", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2015-09-14", WorkInUnitTime = "2015-10-28", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000053", DepartmentId = "检验科", FirstName = "卢钊华", GenderId = "女", IdentityNumber = "513124199407154926", DateOfBirth = "1994-07-15", CityId = "四川汉源", NationId = "汉", PhoneNumber = "17380643302", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "医学检验", GraduatedTime = "", StartWorkTime = "2016-10-16", WorkInUnitTime = "2016-10-16", ProfessionalQualificationId = "检验师", GetProfessionalTime = "", RecruitmentPositionId = "检验师", QualificationId = "检验师", ProfessionalCategoryId = "检验", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000054", DepartmentId = "公卫科", FirstName = "周莉莎", GenderId = "女", IdentityNumber = "513424199309050046", DateOfBirth = "1993-09-05", CityId = "四川德昌", NationId = "汉", PhoneNumber = "13540668296", PoliticalStatusId = "中共党员", JoinPartyTime = "2014-06-01", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "预防医学", GraduatedTime = "", StartWorkTime = "2016-12-01", WorkInUnitTime = "2016-12-01", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "公卫", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000055", DepartmentId = "口腔科", FirstName = "马琳", GenderId = "女", IdentityNumber = "511381199510060027", DateOfBirth = "1995-10-06", CityId = "四川阆中", NationId = "汉", PhoneNumber = "15108455156", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "大专(在职）", GraduatedSchoolId = "山东力明科技职业学院", GraduatedMajorId = "口腔医学", GraduatedTime = "", StartWorkTime = "2016-12-01", WorkInUnitTime = "2016-12-01", ProfessionalQualificationId = "医士", GetProfessionalTime = "", RecruitmentPositionId = "医士", QualificationId = "执业助理医师", ProfessionalCategoryId = "口腔", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000056", DepartmentId = "医务科", FirstName = "冯辉豪", GenderId = "男", IdentityNumber = "510402198710260933", DateOfBirth = "1987-10-26", CityId = "四川攀枝花", NationId = "汉", PhoneNumber = "18980465919", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "硕士", GraduatedSchoolId = "遵义医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2015-07-01", WorkInUnitTime = "2016-12-01", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000057", DepartmentId = "中医科", FirstName = "陈亚娟", GenderId = "女", IdentityNumber = "511024198905293564", DateOfBirth = "1989-05-29", CityId = "四川内江", NationId = "汉", PhoneNumber = "15808587552", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "成都体育学院", GraduatedMajorId = "中医学专业（中医骨伤科学方向）", GraduatedTime = "", StartWorkTime = "2013-08-01", WorkInUnitTime = "2017-11-01", ProfessionalQualificationId = "基层主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000058", DepartmentId = "影像科", FirstName = "刘慧玲", GenderId = "女", IdentityNumber = "510902198901238461", DateOfBirth = "1989-01-23", CityId = "四川遂宁船山区", NationId = "汉", PhoneNumber = "18382586481", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "干部", HighestEducationId = "本科", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "影像", GraduatedTime = "", StartWorkTime = "2017-01-01", WorkInUnitTime = "2019-02-25", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000060", DepartmentId = "中医科", FirstName = "谢紫凌", GenderId = "女", IdentityNumber = "360302199105304521", DateOfBirth = "1991-05-30", CityId = "江西萍乡", NationId = "汉", PhoneNumber = "15281088650", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "硕士", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "中医学", GraduatedTime = "", StartWorkTime = "2018-02-01", WorkInUnitTime = "2020-03-03", ProfessionalQualificationId = "主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "主治中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000061", DepartmentId = "中医科", FirstName = "刘红伟", GenderId = "男", IdentityNumber = "510122198504222871", DateOfBirth = "1985-04-22", CityId = "四川双流", NationId = "汉", PhoneNumber = "18982016865", PoliticalStatusId = "中共党员", JoinPartyTime = "2008-12-04", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "针灸推拿", GraduatedTime = "", StartWorkTime = "2010-07-01", WorkInUnitTime = "2020-03-03", ProfessionalQualificationId = "基层主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000062", DepartmentId = "门诊部", FirstName = "刘伟", GenderId = "男", IdentityNumber = "510183198707152116", DateOfBirth = "1987-07-15", CityId = "四川邛崃", NationId = "汉", PhoneNumber = "18280498971", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川大学", GraduatedMajorId = "临床医学", GraduatedTime = "2017-06-29", StartWorkTime = "2011-09-01", WorkInUnitTime = "2020-03-03", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000063", DepartmentId = "儿科", FirstName = "李源", GenderId = "女", IdentityNumber = "430802198812210023", DateOfBirth = "1988-12-21", CityId = "湖南张家界", NationId = "土家族", PhoneNumber = "15184338577", PoliticalStatusId = "中共党员", JoinPartyTime = "2010-05-20", CadreStatusId = "", HighestEducationId = "硕士", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "针灸学", GraduatedTime = "", StartWorkTime = "2014-01-01", WorkInUnitTime = "2020-12-26", ProfessionalQualificationId = "主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "主治中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000064", DepartmentId = "中医科", FirstName = "谢晓容", GenderId = "女", IdentityNumber = "511023199001111623", DateOfBirth = "1990-01-11", CityId = "四川资阳", NationId = "汉", PhoneNumber = "18080897437", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "硕士", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "中西医结合临床", GraduatedTime = "", StartWorkTime = "2017-11-01", WorkInUnitTime = "2021-12-13", ProfessionalQualificationId = "主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000065", DepartmentId = "口腔科", FirstName = "田夕然", GenderId = "女", IdentityNumber = "510182199506200028", DateOfBirth = "1995-06-20", CityId = "四川彭州", NationId = "汉", PhoneNumber = "17338902533", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "济宁医学院", GraduatedMajorId = "口腔医学", GraduatedTime = "", StartWorkTime = "2018-12-01", WorkInUnitTime = "2021-12-13", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "口腔", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000066", DepartmentId = "副主任", FirstName = "何晓军", GenderId = "男", IdentityNumber = "510132198502227516", DateOfBirth = "1985-02-22", CityId = "四川新津", NationId = "汉", PhoneNumber = "13408654885", PoliticalStatusId = "中共党员", JoinPartyTime = "2008-10-27", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "四川大学", GraduatedMajorId = "预防医学", GraduatedTime = "", StartWorkTime = "2010-12-01", WorkInUnitTime = "2022-02-23", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "公卫", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000067", DepartmentId = "妇科", FirstName = "施雯霖", GenderId = "女", IdentityNumber = "652301199605270027", DateOfBirth = "1996-05-27", CityId = "新疆昌吉", NationId = "汉", PhoneNumber = "18340809870", PoliticalStatusId = "团员", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "硕士", GraduatedSchoolId = "大连医科大学", GraduatedMajorId = "妇产科学", GraduatedTime = "2022-06-30", StartWorkTime = "2022-11-01", WorkInUnitTime = "2022-07-21", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000068", DepartmentId = "儿科", FirstName = "郭煚垚", GenderId = "男", IdentityNumber = "513001199408040832", DateOfBirth = "1994-08-04", CityId = "四川成都", NationId = "汉", PhoneNumber = "15821280141", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "蚌埠医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2019-11-01", WorkInUnitTime = "2022-08-08", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000069", DepartmentId = "公卫科", FirstName = "王丽丽", GenderId = "女", IdentityNumber = "510106199009211424", DateOfBirth = "1990-09-21", CityId = "四川成都", NationId = "汉族", PhoneNumber = "15884587628", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "预防医学", GraduatedTime = "", StartWorkTime = "2015-07-01", WorkInUnitTime = "2022-08-09", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "公卫", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000070", DepartmentId = "检验科", FirstName = "梁殊荣", GenderId = "女", IdentityNumber = "510723199604084983", DateOfBirth = "1996-04-08", CityId = "四川盐亭", NationId = "汉族", PhoneNumber = "18408275028", PoliticalStatusId = "共产党员", JoinPartyTime = "2021-07-01", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "医学检验", GraduatedTime = "", StartWorkTime = "2018-02-01", WorkInUnitTime = "2022-09-05", ProfessionalQualificationId = "检验师", GetProfessionalTime = "", RecruitmentPositionId = "检验师", QualificationId = "检验师", ProfessionalCategoryId = "检验", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000071", DepartmentId = "住院部", FirstName = "李晋全", GenderId = "男", IdentityNumber = "511381198809170250", DateOfBirth = "1988-09-17", CityId = "四川阆中", NationId = "汉族", PhoneNumber = "13551159722", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2014-11-01", WorkInUnitTime = "2022-09-17", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000072", DepartmentId = "住院部", FirstName = "单凤", GenderId = "女", IdentityNumber = "510902199112143106", DateOfBirth = "1991-12-14", CityId = "四川遂宁", NationId = "汉", PhoneNumber = "18215521298", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川大学青羊校区", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2017-01-01", WorkInUnitTime = "2022-09-19", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000073", DepartmentId = "全科", FirstName = "陈鹏", GenderId = "男", IdentityNumber = "510704199110283318", DateOfBirth = "1991-10-28", CityId = "四川绵阳", NationId = "汉", PhoneNumber = "18202877362", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "临床医学", GraduatedTime = "2016-06-26", StartWorkTime = "2016-10-01", WorkInUnitTime = "2022-09-26", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000074", DepartmentId = "住院部", FirstName = "肖兵", GenderId = "男", IdentityNumber = "510723198804241918", DateOfBirth = "1988-04-24", CityId = "四川绵阳", NationId = "汉", PhoneNumber = "13679089168", PoliticalStatusId = "中共党员", JoinPartyTime = "2014.6.24", CadreStatusId = "", HighestEducationId = "研究生（在职）", GraduatedSchoolId = "四川大学", GraduatedMajorId = "急诊医学", GraduatedTime = "", StartWorkTime = "2018-09-01", WorkInUnitTime = "2022-09-30", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000075", DepartmentId = "全科", FirstName = "刘悦", GenderId = "女", IdentityNumber = "510525199009308681", DateOfBirth = "1990-09-30", CityId = "四川泸州", NationId = "汉", PhoneNumber = "15902887435", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2016-10-01", WorkInUnitTime = "2022-10-08", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000076", DepartmentId = "住院部", FirstName = "隆倩", GenderId = "女", IdentityNumber = "511025198805230781", DateOfBirth = "1988-05-23", CityId = "四川内江", NationId = "汉", PhoneNumber = "13551853365", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "临床医学", GraduatedTime = "2012-07-01", StartWorkTime = "2015-06-01", WorkInUnitTime = "2022-10-11", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000077", DepartmentId = "运管科", FirstName = "田川", GenderId = "男", IdentityNumber = "510722199312234197", DateOfBirth = "1993-12-23", CityId = "四川绵阳", NationId = "汉", PhoneNumber = "15208424672", PoliticalStatusId = "中共党员", JoinPartyTime = "2014-12-08", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "临床医学", GraduatedTime = "2016-06-23", StartWorkTime = "2016-09-01", WorkInUnitTime = "2022-10-11", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000078", DepartmentId = "住院部", FirstName = "文飞翔", GenderId = "女", IdentityNumber = "511622199210211328", DateOfBirth = "1992-10-21", CityId = "四川成都", NationId = "汉", PhoneNumber = "15881763190", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "川北医学院", GraduatedMajorId = "临床医学", GraduatedTime = "2017-07-01", StartWorkTime = "2018-01-01", WorkInUnitTime = "2022-11-03", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000079", DepartmentId = "儿保科", FirstName = "向星", GenderId = "女", IdentityNumber = "51092119910701072X", DateOfBirth = "1991-07-01", CityId = "四川遂宁", NationId = "汉", PhoneNumber = "18280439810", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川大学", GraduatedMajorId = "临床医学", GraduatedTime = "2019-06-30", StartWorkTime = "2015-09-01", WorkInUnitTime = "2022-11-07", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000080", DepartmentId = "影像科", FirstName = "王苏秀", GenderId = "女", IdentityNumber = "410523198904103024", DateOfBirth = "1989-04-10", CityId = "河南汤阴", NationId = "汉", PhoneNumber = "18780266802", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "泰山医学院", GraduatedMajorId = "医学影像学", GraduatedTime = "", StartWorkTime = "2015-03-01", WorkInUnitTime = "2021-05-01", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000081", DepartmentId = "皮肤科", FirstName = "蒋丽", GenderId = "女", IdentityNumber = "510922199408151880", DateOfBirth = "1994-08-15", CityId = "四川遂宁", NationId = "汉族", PhoneNumber = "17778380662", PoliticalStatusId = "中共党员", JoinPartyTime = "1905-07-10", CadreStatusId = "", HighestEducationId = "硕士", GraduatedSchoolId = "中南大学", GraduatedMajorId = "皮肤病与性病学", GraduatedTime = "2023.6.7", StartWorkTime = "", WorkInUnitTime = "2023-08-14", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000083", DepartmentId = "副主任", FirstName = "杜海", GenderId = "男", IdentityNumber = "51012219811102003X", DateOfBirth = "1981-11-01", CityId = "四川双流", NationId = "汉", PhoneNumber = "13880812321", PoliticalStatusId = "中共党员", JoinPartyTime = "2007.06.06", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "中西医临床", GraduatedTime = "1905-07-07", StartWorkTime = "1999-09-01", WorkInUnitTime = "2022-01-24", ProfessionalQualificationId = "主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "主治中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000084", DepartmentId = "运管科", FirstName = "柴颖", GenderId = "女", IdentityNumber = "51012219840622702X", DateOfBirth = "1984-06-22", CityId = "四川双流", NationId = "汉", PhoneNumber = "13658009766", PoliticalStatusId = "中共党员", JoinPartyTime = "2015.06.19", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川农业大学", GraduatedMajorId = "人力资源管理", GraduatedTime = "", StartWorkTime = "2003-12-01", WorkInUnitTime = "2021-11-22", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000085", DepartmentId = "药剂科", FirstName = "程仙蓉", GenderId = "女", IdentityNumber = "510122198010187543", DateOfBirth = "1980-10-18", CityId = "四川双流", NationId = "汉", PhoneNumber = "13551395740", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "成都卫校", GraduatedMajorId = "药剂", GraduatedTime = "", StartWorkTime = "1999-09-01", WorkInUnitTime = "2009-4", ProfessionalQualificationId = "主管药剂师", GetProfessionalTime = "", RecruitmentPositionId = "药剂师", QualificationId = "执业药师", ProfessionalCategoryId = "药学", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000086", DepartmentId = "全科", FirstName = "杨剑平", GenderId = "女", IdentityNumber = "513425197105318327", DateOfBirth = "1971-05-31", CityId = "四川双流", NationId = "汉", PhoneNumber = "15982379621", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "工人", HighestEducationId = "大专", GraduatedSchoolId = "广元职工医学院", GraduatedMajorId = "临床", GraduatedTime = "", StartWorkTime = "1992-08-30", WorkInUnitTime = "2007-8", ProfessionalQualificationId = "副主任医师", GetProfessionalTime = "", RecruitmentPositionId = "副主任医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "副高", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000087", DepartmentId = "计免科", FirstName = "张艳", GenderId = "女", IdentityNumber = "510122198201131921", DateOfBirth = "1982-01-13", CityId = "四川双流", NationId = "汉", PhoneNumber = "13408461981", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2007-09-01", WorkInUnitTime = "2009-7", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000088", DepartmentId = "体检科", FirstName = "尹玲", GenderId = "女", IdentityNumber = "510122197905150026", DateOfBirth = "1979-05-15", CityId = "四川双流", NationId = "汉", PhoneNumber = "13540698988", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "1998-11-01", WorkInUnitTime = "2009-9", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理 ", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000089", DepartmentId = "体检科", FirstName = "郑军", GenderId = "男", IdentityNumber = "510122198509223371", DateOfBirth = "1985-09-22", CityId = "四川双流", NationId = "汉", PhoneNumber = "17761258024", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "泸州医学院", GraduatedMajorId = "中医学（中国医科大学药学）", GraduatedTime = "", StartWorkTime = "2008-04-03", WorkInUnitTime = "2008-04-03", ProfessionalQualificationId = "药剂士", GetProfessionalTime = "", RecruitmentPositionId = "药剂士", QualificationId = "药剂士", ProfessionalCategoryId = "药学", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000090", DepartmentId = "中医科", FirstName = "郑丽辉", GenderId = "女", IdentityNumber = "411224198502210020", DateOfBirth = "1985-02-21", CityId = "河南三门峡", NationId = "汉", PhoneNumber = "18602889435", PoliticalStatusId = "民革", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "中医学", GraduatedTime = "", StartWorkTime = "2007-08-01", WorkInUnitTime = "2007-08-01", ProfessionalQualificationId = "主治中医师", GetProfessionalTime = "", RecruitmentPositionId = "主治中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000091", DepartmentId = "检验科", FirstName = "胡国兰", GenderId = "女", IdentityNumber = "510212198012290846", DateOfBirth = "1980-12-29", CityId = "四川双流", NationId = "汉", PhoneNumber = "18030408020", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "医学检验", GraduatedTime = "", StartWorkTime = "2007-01-01", WorkInUnitTime = "2007-01-01", ProfessionalQualificationId = "主管检验师", GetProfessionalTime = "", RecruitmentPositionId = "主管检验师", QualificationId = "检验师（师）", ProfessionalCategoryId = "检验", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000092", DepartmentId = "药剂科", FirstName = "孙丽娜", GenderId = "女", IdentityNumber = "510122198701230029", DateOfBirth = "1987-01-23", CityId = "四川双流", NationId = "汉", PhoneNumber = "15908150492", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "药学技术", GraduatedTime = "", StartWorkTime = "2008-10-08", WorkInUnitTime = "2008-10-08", ProfessionalQualificationId = "药剂师", GetProfessionalTime = "", RecruitmentPositionId = "药剂师", QualificationId = "药剂师", ProfessionalCategoryId = "药学", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000093", DepartmentId = "病区护士", FirstName = "廖茜", GenderId = "女", IdentityNumber = "513028197810052728", DateOfBirth = "1978-10-05", CityId = "四川巴中", NationId = "汉", PhoneNumber = "13094440388", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2007-08-01", WorkInUnitTime = "2007-08-01", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000094", DepartmentId = "计免科", FirstName = "李华香", GenderId = "女", IdentityNumber = "510122198905176626", DateOfBirth = "1989-05-17", CityId = "四川双流", NationId = "汉", PhoneNumber = "13408655954", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2007-07-01", WorkInUnitTime = "2007-07-01", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000095", DepartmentId = "运管科", FirstName = "李园", GenderId = "女", IdentityNumber = "510122198712171765", DateOfBirth = "1987-12-17", CityId = "四川双流", NationId = "汉族", PhoneNumber = "13668283836", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2007-04-12", WorkInUnitTime = "2007-04-12", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000096", DepartmentId = "病区护士", FirstName = "周玉", GenderId = "女", IdentityNumber = "510122198607286421", DateOfBirth = "1986-07-28", CityId = "四川双流", NationId = "汉族", PhoneNumber = "13568983604", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2007-06-11", WorkInUnitTime = "2007-06-11", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000097", DepartmentId = "病区护士", FirstName = "罗凤兰", GenderId = "女", IdentityNumber = "510902198610158509", DateOfBirth = "1986-10-15", CityId = "四川遂宁", NationId = "汉族", PhoneNumber = "18328688038", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2008-09-01", WorkInUnitTime = "2008-09-01", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000098", DepartmentId = "计免科", FirstName = "徐海燕", GenderId = "女", IdentityNumber = "500234198910196081", DateOfBirth = "1989-10-19", CityId = "重庆", NationId = "汉族", PhoneNumber = "13679050103", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2008-09-07", WorkInUnitTime = "2008-09-07", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000099", DepartmentId = "病区护士", FirstName = "韦娟", GenderId = "女", IdentityNumber = "513022198704081863", DateOfBirth = "1987-04-08", CityId = "四川达州", NationId = "汉", PhoneNumber = "13730661920", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2009-03-11", WorkInUnitTime = "2009-03-11", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000100", DepartmentId = "计免科", FirstName = "刘琴", GenderId = "女", IdentityNumber = "511113198602280324", DateOfBirth = "1986-02-28", CityId = "四川乐山", NationId = "汉", PhoneNumber = "15208226996", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2009-03-11", WorkInUnitTime = "2009-03-11", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000101", DepartmentId = "病区护士", FirstName = "龚秀英", GenderId = "女", IdentityNumber = "500382198803016002", DateOfBirth = "1988-03-01", CityId = "重庆合川", NationId = "汉", PhoneNumber = "13550057628", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2009-03-12", WorkInUnitTime = "2009-03-12", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000102", DepartmentId = "计免科", FirstName = "李小英", GenderId = "女", IdentityNumber = "51303019751129362X", DateOfBirth = "1975-11-29", CityId = "四川双流", NationId = "汉", PhoneNumber = "13981828929", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2009-03-16", WorkInUnitTime = "2009-03-16", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000103", DepartmentId = "病区护士", FirstName = "熊小琴", GenderId = "女", IdentityNumber = "510122198712064126", DateOfBirth = "1987-12-06", CityId = "四川双流", NationId = "汉", PhoneNumber = "13699079776", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2009-04-13", WorkInUnitTime = "2009-04-13", ProfessionalQualificationId = "基层主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000104", DepartmentId = "儿保科", FirstName = "李德辉", GenderId = "女", IdentityNumber = "51100219761018224X", DateOfBirth = "1976-10-18", CityId = "四川资阳", NationId = "汉", PhoneNumber = "15928459086", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2008-10-13", WorkInUnitTime = "2008-10-13", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000105", DepartmentId = "计免科", FirstName = "朱萍", GenderId = "女", IdentityNumber = "510122198910280065", DateOfBirth = "1989-10-28", CityId = "四川双流", NationId = "汉", PhoneNumber = "13648096809", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2009-06-01", WorkInUnitTime = "2009-06-01", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000106", DepartmentId = "儿保科", FirstName = "周东琴", GenderId = "女", IdentityNumber = "511102198702257720", DateOfBirth = "1987-02-25", CityId = "四川乐山", NationId = "汉", PhoneNumber = "18980834925", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2009-06-01", WorkInUnitTime = "2009-06-01", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000107", DepartmentId = "药剂科", FirstName = "赵元源", GenderId = "男", IdentityNumber = "510122199201260277", DateOfBirth = "1992-01-26", CityId = "四川双流", NationId = "汉", PhoneNumber = "13908083149", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专(在职）", GraduatedSchoolId = "成都中医药大学附属针灸学院（国家开放大学", GraduatedMajorId = "药学）", GraduatedTime = "", StartWorkTime = "2010-07-22", WorkInUnitTime = "2010-07-22", ProfessionalQualificationId = "药剂士", GetProfessionalTime = "", RecruitmentPositionId = "药剂士", QualificationId = "药剂士", ProfessionalCategoryId = "药学", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000108", DepartmentId = "儿保科", FirstName = "李翠柏", GenderId = "女", IdentityNumber = "51132219921119704X", DateOfBirth = "1992-11-19", CityId = "四川南充", NationId = "汉", PhoneNumber = "13438919142", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川大学", GraduatedMajorId = " 护理学", GraduatedTime = "", StartWorkTime = "2011-01-04", WorkInUnitTime = "2011-01-04", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000109", DepartmentId = "体检科", FirstName = "简静", GenderId = "女", IdentityNumber = "52011119741017482X", DateOfBirth = "1974-10-17", CityId = "四川重庆", NationId = "汉", PhoneNumber = "18781996191", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "贵洲省贵阳市贵医附院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2011-03-30", WorkInUnitTime = "2011-03-30", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000110", DepartmentId = "全科", FirstName = "倪付燕", GenderId = "女", IdentityNumber = "500238198901174409", DateOfBirth = "1989-01-17", CityId = "四川重庆", NationId = "汉", PhoneNumber = "18780260652", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "护理", GraduatedTime = "2023.1.30", StartWorkTime = "2011-06-22", WorkInUnitTime = "2011-06-22", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000111", DepartmentId = "妇保科", FirstName = "刘莉莉", GenderId = "女", IdentityNumber = "513333198601260020", DateOfBirth = "1986-01-26", CityId = "四川双流", NationId = "汉", PhoneNumber = "15680788883", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专(在职）", GraduatedSchoolId = "中央广播电视大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2011-08-15", WorkInUnitTime = "2011-08-15", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000112", DepartmentId = "全科", FirstName = "陈蕾", GenderId = "女", IdentityNumber = "510122198903294928", DateOfBirth = "1989-03-29", CityId = "四川双流", NationId = "汉", PhoneNumber = "18980989465", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "护理", GraduatedTime = "2023.1.30", StartWorkTime = "2011-11-23", WorkInUnitTime = "2011-11-23", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000113", DepartmentId = "全科", FirstName = "汪健", GenderId = "女", IdentityNumber = "51370119880729434X ", DateOfBirth = "1988-07-29", CityId = "四川巴中", NationId = "汉", PhoneNumber = "17713405436", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2011-11-23", WorkInUnitTime = "2011-11-23", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000114", DepartmentId = "病区护士", FirstName = "何霞", GenderId = "女", IdentityNumber = "510522198807050044", DateOfBirth = "1988-07-05", CityId = "四川泸州", NationId = "汉", PhoneNumber = "18081168059", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2011-11-23", WorkInUnitTime = "2011-11-23", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000115", DepartmentId = "计免科", FirstName = "刘慧平", GenderId = "女", IdentityNumber = "510107198712023427", DateOfBirth = "1987-12-02", CityId = "四川成都", NationId = "汉", PhoneNumber = "18111561095", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "川北医学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2013-05-24", WorkInUnitTime = "2013-05-24", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000116", DepartmentId = "儿保科", FirstName = "王倩", GenderId = "女", IdentityNumber = "510184199109062429", DateOfBirth = "1991-09-06", CityId = "四川崇州", NationId = "汉", PhoneNumber = "15184494684", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2014-01-08", WorkInUnitTime = "2014-01-08", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000117", DepartmentId = "检验科", FirstName = "刘锐", GenderId = "男", IdentityNumber = "510122198805213370", DateOfBirth = "1988-05-21", CityId = "四川双流", NationId = "汉", PhoneNumber = "13551303477", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "雅安职业技术学院", GraduatedMajorId = "医学检验", GraduatedTime = "", StartWorkTime = "2014-05-12", WorkInUnitTime = "2014-05-12", ProfessionalQualificationId = "检验师", GetProfessionalTime = "", RecruitmentPositionId = "检验师", QualificationId = "检验师", ProfessionalCategoryId = "检验", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000118", DepartmentId = "门诊部", FirstName = "周凯", GenderId = "男", IdentityNumber = "511027198101032050", DateOfBirth = "1981-01-03", CityId = "四川成都", NationId = "汉", PhoneNumber = "13980433175", PoliticalStatusId = "中共党员", JoinPartyTime = "2010-07-01", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2014-09-09", WorkInUnitTime = "2014-09-09", ProfessionalQualificationId = "基层主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000119", DepartmentId = "儿保科", FirstName = "周燕", GenderId = "女", IdentityNumber = "513721198805175182", DateOfBirth = "1988-05-17", CityId = "四川巴中", NationId = "汉", PhoneNumber = "15281851222", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "达州职业技术学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2014-12-22", WorkInUnitTime = "2014-12-22", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000120", DepartmentId = "信息科", FirstName = "张晓峰", GenderId = "男", IdentityNumber = "510107198604076230", DateOfBirth = "1986-04-07", CityId = "四川成都", NationId = "汉", PhoneNumber = "18180777212", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "西安交通大学", GraduatedMajorId = "药学", GraduatedTime = "", StartWorkTime = "2015-02-25", WorkInUnitTime = "2015-02-25", ProfessionalQualificationId = "药剂士", GetProfessionalTime = "", RecruitmentPositionId = "药剂士", QualificationId = "药剂士", ProfessionalCategoryId = "药学", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000121", DepartmentId = "总务科", FirstName = "杜琪", GenderId = "女", IdentityNumber = "510681198908301127", DateOfBirth = "1989-08-30", CityId = "四川成都", NationId = "汉", PhoneNumber = "13408610338", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川广播电视大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2015-04-20", WorkInUnitTime = "2015-04-20", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000122", DepartmentId = "病区护士", FirstName = "张艳", GenderId = "女", IdentityNumber = "513425199310265227", DateOfBirth = "1993-10-26", CityId = "四川会理", NationId = "汉", PhoneNumber = "18380466621", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2015-06-08", WorkInUnitTime = "2015-09-10", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000123", DepartmentId = "公卫科", FirstName = "李丹", GenderId = "女", IdentityNumber = "500223199411026681", DateOfBirth = "1994-11-02", CityId = "重庆潼南", NationId = "汉", PhoneNumber = "13438475967", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "中专", GraduatedSchoolId = "四川省人民医院护理学校", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2015-08-05", WorkInUnitTime = "2015-08-05", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000124", DepartmentId = "全科", FirstName = "赵婷", GenderId = "女", IdentityNumber = "513721199410072204", DateOfBirth = "1994-10-07", CityId = "四川通江", NationId = "汉", PhoneNumber = "17713538630", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2015-09-10", WorkInUnitTime = "2015-09-10", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000125", DepartmentId = "医保办", FirstName = "游佳琪", GenderId = "女", IdentityNumber = "51018319951229432X", DateOfBirth = "1995-12-29", CityId = "四川邛崃", NationId = "汉", PhoneNumber = "15881149182", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2016-06-06", WorkInUnitTime = "2016-06-06", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000126", DepartmentId = "儿保科", FirstName = "杨瑞", GenderId = "女", IdentityNumber = "510122199603058127", DateOfBirth = "1996-03-05", CityId = "四川双流", NationId = "汉", PhoneNumber = "18728481094", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2016-09-13", WorkInUnitTime = "2016-09-13", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000127", DepartmentId = "医保办", FirstName = "易菊花", GenderId = "女", IdentityNumber = "513822199001087706", DateOfBirth = "1990-01-08", CityId = "四川双流", NationId = "汉", PhoneNumber = "19983176137", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2016-10-12", WorkInUnitTime = "2016-10-12", ProfessionalQualificationId = "基层主管护师", GetProfessionalTime = "2021.4.18", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000128", DepartmentId = "病区护士", FirstName = "杨萍", GenderId = "女", IdentityNumber = "510122198310134929", DateOfBirth = "1983-10-13", CityId = "四川双流", NationId = "汉", PhoneNumber = "18982018047", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2017-06-01", WorkInUnitTime = "2017-06-01", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000129", DepartmentId = "中医科", FirstName = "赵胜建", GenderId = "男", IdentityNumber = "510902199108025617", DateOfBirth = "1991-08-02", CityId = "四川双流", NationId = "汉", PhoneNumber = "17381953036", PoliticalStatusId = "中共党员", JoinPartyTime = "2020-11-23", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "辽宁中医药大学杏林学院", GraduatedMajorId = "针灸推拿学", GraduatedTime = "", StartWorkTime = "2017-11-01", WorkInUnitTime = "2017-11-01", ProfessionalQualificationId = "中医师", GetProfessionalTime = "", RecruitmentPositionId = "中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000130", DepartmentId = "中医科", FirstName = "王超", GenderId = "女", IdentityNumber = "510122198904048120", DateOfBirth = "1989-04-04", CityId = "四川双流", NationId = "汉", PhoneNumber = "13880016246", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "北京中医药大学", GraduatedMajorId = "针灸推拿学", GraduatedTime = "", StartWorkTime = "2018-05-14", WorkInUnitTime = "2019-02-18", ProfessionalQualificationId = "中医师", GetProfessionalTime = "", RecruitmentPositionId = "中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000131", DepartmentId = "体检科", FirstName = "李雪萍", GenderId = "女", IdentityNumber = "510122199212164125", DateOfBirth = "1992-12-16", CityId = "四川双流", NationId = "汉", PhoneNumber = "13518140830", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2018-06-18", WorkInUnitTime = "2019-02-18", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000132", DepartmentId = "检验科", FirstName = "张露", GenderId = "女", IdentityNumber = "510132198711186626", DateOfBirth = "1987-11-18", CityId = "四川新津", NationId = "汉", PhoneNumber = "15928945591", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "医学检验", GraduatedTime = "", StartWorkTime = "2019-03-21", WorkInUnitTime = "2019-04-11", ProfessionalQualificationId = "检验师", GetProfessionalTime = "", RecruitmentPositionId = "检验师", QualificationId = "检验师", ProfessionalCategoryId = "检验", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000133", DepartmentId = "药剂科", FirstName = "任昭玥", GenderId = "女", IdentityNumber = "51041119960601002x", DateOfBirth = "1996-06-01", CityId = "重庆涪陵", NationId = "汉", PhoneNumber = "17775524176", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "雅安职业技术学院", GraduatedMajorId = "药学", GraduatedTime = "", StartWorkTime = "2019-05-14", WorkInUnitTime = "2019-07-01", ProfessionalQualificationId = "药剂士", GetProfessionalTime = "", RecruitmentPositionId = "药剂士", QualificationId = "药剂士", ProfessionalCategoryId = "药学", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000134", DepartmentId = "病区护士", FirstName = "罗雪", GenderId = "女", IdentityNumber = "513822200005217307", DateOfBirth = "2000-05-21", CityId = "四川仁寿", NationId = "汉", PhoneNumber = "13551258029", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "中专", GraduatedSchoolId = "四川大学附设华西卫生学校", GraduatedMajorId = "", GraduatedTime = "", StartWorkTime = "2019-11-25", WorkInUnitTime = "2020-08-01", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000135", DepartmentId = "病区护士", FirstName = "喻学梅", GenderId = "女", IdentityNumber = "510129198601072128", DateOfBirth = "1986-01-07", CityId = "四川大邑", NationId = "汉", PhoneNumber = "13880536070", PoliticalStatusId = "中共党员", JoinPartyTime = "2018.11.5", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "承德医学院", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2020-05-06", WorkInUnitTime = "2020-07-15", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000136", DepartmentId = "药剂科", FirstName = "杨卓", GenderId = "男", IdentityNumber = "510122199707288314", DateOfBirth = "1997-07-28", CityId = "四川双流", NationId = "汉", PhoneNumber = "17828099806", PoliticalStatusId = "团员", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "药学", GraduatedTime = "", StartWorkTime = "2020-10-13", WorkInUnitTime = "2021-02-10", ProfessionalQualificationId = "药剂士", GetProfessionalTime = "", RecruitmentPositionId = "药剂士", QualificationId = "药剂士", ProfessionalCategoryId = "药学", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000137", DepartmentId = "口腔科", FirstName = "钱晨", GenderId = "女", IdentityNumber = "540102199605291029", DateOfBirth = "1996-05-29", CityId = "四川绵阳", NationId = "汉", PhoneNumber = "18351991191", PoliticalStatusId = "团员", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "南京医科大学", GraduatedMajorId = "口腔医学", GraduatedTime = "", StartWorkTime = "2020-10-26", WorkInUnitTime = "2021-02-10", ProfessionalQualificationId = "医师", GetProfessionalTime = "", RecruitmentPositionId = "医师", QualificationId = "执业医师", ProfessionalCategoryId = "口腔", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000138", DepartmentId = "病区护士", FirstName = "宋玉婷", GenderId = "女", IdentityNumber = "500108199506030828", DateOfBirth = "1995-06-03", CityId = "重庆", NationId = "汉", PhoneNumber = "17388285190", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "中国医科大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2021-07-05", WorkInUnitTime = "2021-08-10", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000139", DepartmentId = "病区护士", FirstName = "周钰馨", GenderId = "女", IdentityNumber = "510107199812225426", DateOfBirth = "1998-12-22", CityId = "四川成都", NationId = "汉", PhoneNumber = "18161343227", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "四川工业科技学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2021-07-15", WorkInUnitTime = "2021-08-10", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000140", DepartmentId = "病区护士", FirstName = "白雪", GenderId = "女", IdentityNumber = "510132199512270627", DateOfBirth = "1995-12-27", CityId = "四川新津", NationId = "汉", PhoneNumber = "18030583918", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "长沙民政职业技术学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2022-06-01", WorkInUnitTime = "2022-06-01", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000141", DepartmentId = "病区护士", FirstName = "吴婷", GenderId = "女", IdentityNumber = "511322199910122186", DateOfBirth = "1999-10-22", CityId = "四川成都", NationId = "汉", PhoneNumber = "15828472687", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "四川护理职业学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2022-06-01", WorkInUnitTime = "2022-06-01", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000142", DepartmentId = "住院部", FirstName = "甯雨露", GenderId = "女", IdentityNumber = "510122198609083222", DateOfBirth = "1986-09-08", CityId = "四川双流", NationId = "汉", PhoneNumber = "15928514725", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "川北医学院", GraduatedMajorId = "临床医学", GraduatedTime = "2010-07-01", StartWorkTime = "2022-06-20", WorkInUnitTime = "2022-12-12", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000143", DepartmentId = "药剂科", FirstName = "陈才", GenderId = "男", IdentityNumber = "513030199109165119", DateOfBirth = "1991-09-16", CityId = "四川成都", NationId = "汉", PhoneNumber = "18782976965", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "中药学", GraduatedTime = "", StartWorkTime = "2022-12-12", WorkInUnitTime = "2022-12-12", ProfessionalQualificationId = "主管中药师", GetProfessionalTime = "", RecruitmentPositionId = "主管中药师", QualificationId = "主管药剂师", ProfessionalCategoryId = "药学", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000144", DepartmentId = "中医科", FirstName = "李欢", GenderId = "女", IdentityNumber = "510525199308207901", DateOfBirth = "1993-08-20", CityId = "四川泸州", NationId = "汉", PhoneNumber = "13647813044", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "广西中医药大学赛恩斯新医药学院", GraduatedMajorId = "中医学", GraduatedTime = "", StartWorkTime = "2022-12-12", WorkInUnitTime = "2022-12-12", ProfessionalQualificationId = "中医师", GetProfessionalTime = "", RecruitmentPositionId = "中医师", QualificationId = "执业医师", ProfessionalCategoryId = "中医", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000145", DepartmentId = "医务科", FirstName = "曾春蕾", GenderId = "女", IdentityNumber = "510121199204030024", DateOfBirth = "1992-04-03", CityId = "四川成都", NationId = "汉", PhoneNumber = "13699411344", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都医学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2022-12-12", WorkInUnitTime = "2022-12-12", ProfessionalQualificationId = "主管护师", GetProfessionalTime = "", RecruitmentPositionId = "主管护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000146", DepartmentId = "护理部", FirstName = "杨鑫桃", GenderId = "女", IdentityNumber = "513423199502156508", DateOfBirth = "1995-02-15", CityId = "四川盐源", NationId = "汉", PhoneNumber = "18328030713", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "四川大学", GraduatedMajorId = "护理学", GraduatedTime = "", StartWorkTime = "2022-12-12", WorkInUnitTime = "2022-12-12", ProfessionalQualificationId = "护师", GetProfessionalTime = "", RecruitmentPositionId = "护师", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000147", DepartmentId = "全科", FirstName = "张苗苗", GenderId = "女", IdentityNumber = "510122199501300023", DateOfBirth = "1995-01-30", CityId = "四川双流", NationId = "汉", PhoneNumber = "15008218664", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2022-12-12", WorkInUnitTime = "2022-12-12", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000148", DepartmentId = "护理部", FirstName = "谢菲", GenderId = "女", IdentityNumber = "510122200006190029", DateOfBirth = "2000-06-19", CityId = "四川双流", NationId = "汉", PhoneNumber = "18381056083", PoliticalStatusId = "共产党员", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "广安职业技术学院", GraduatedMajorId = "护理", GraduatedTime = "", StartWorkTime = "2022-12-12", WorkInUnitTime = "2022-12-12", ProfessionalQualificationId = "护士", GetProfessionalTime = "", RecruitmentPositionId = "护士", QualificationId = "执业护士", ProfessionalCategoryId = "护理", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000149", DepartmentId = "中医科", FirstName = "刘家旭", GenderId = "男", IdentityNumber = "511321199309150016", DateOfBirth = "1993-09-15", CityId = "四川成都", NationId = "汉", PhoneNumber = "17738760563", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科(在职）", GraduatedSchoolId = "成都中医药大学", GraduatedMajorId = "康复治疗学", GraduatedTime = "", StartWorkTime = "2022-12-12", WorkInUnitTime = "2023-01-13", ProfessionalQualificationId = "康复治疗师", GetProfessionalTime = "", RecruitmentPositionId = "康复治疗师", QualificationId = "康复技师", ProfessionalCategoryId = "技师", JobTitleId = "初级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000150", DepartmentId = "儿保科", FirstName = "殷晓丽", GenderId = "女", IdentityNumber = "510107198707311264", DateOfBirth = "1987-07-31", CityId = "四川成都", NationId = "汉", PhoneNumber = "17740204330", PoliticalStatusId = "", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "本科", GraduatedSchoolId = "川北医学院", GraduatedMajorId = "临床医学", GraduatedTime = "", StartWorkTime = "2022-12-12", WorkInUnitTime = "2023-01-17", ProfessionalQualificationId = "主治医师", GetProfessionalTime = "", RecruitmentPositionId = "主治医师", QualificationId = "执业医师", ProfessionalCategoryId = "临床", JobTitleId = "中级", IsGeneral = true },
new EmployeeImportEntity { EmpNo = "EN-000151", DepartmentId = "公卫科", FirstName = "冯子豪", GenderId = "男", IdentityNumber = "510104199912074873", DateOfBirth = "1999-12-07", CityId = "四川成都", NationId = "汉族", PhoneNumber = "17608076838", PoliticalStatusId = "团员", JoinPartyTime = "", CadreStatusId = "", HighestEducationId = "大专", GraduatedSchoolId = "四川农业大学", GraduatedMajorId = "药学", GraduatedTime = "", StartWorkTime = "2022-11-01", WorkInUnitTime = "2023-06-07", ProfessionalQualificationId = "药剂士", GetProfessionalTime = "", RecruitmentPositionId = "药剂士", QualificationId = "药剂士", ProfessionalCategoryId = "药学", JobTitleId = "初级", IsGeneral = true }
};
    }

    private static void FeedDesignations(ApplicationDbContext appDbContext)
    {
        appDbContext.Designations.AddRange(
             new Designation { Code = "DSC-000001", Description = "科室主任", Name = "科室主任" },
             new Designation { Code = "DSC-000002", Description = "主治医生", Name = "主治医生" }
         );
        appDbContext.SaveChanges();
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
        appDbContext.SaveChanges();
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
        appDbContext.SaveChanges();
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
        appDbContext.SaveChanges();
    }

    private static void FeedCityCodes(ApplicationDbContext appDbContext)
    {
        var country = appDbContext.Countries.FirstOrDefault();
        appDbContext.Cities.Add(new City { Name = "四川省成都市", Code = "CC-000001", CountryId = country.Id });
        appDbContext.SaveChanges();
    }

    private static void FeedCountryCodes(ApplicationDbContext appDbContext)
    {
        appDbContext.Countries.Add(new Country { Code = "NC-CN", Name = "中华人民共和国" });
        appDbContext.SaveChanges();
    }

    private static void FeedGenders(ApplicationDbContext appDbContext)
    {
        var code = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-GC");
        appDbContext.SystemCodeDetails.AddRange(
            new SystemCodeDetail { Code = "SC-GC-000001", Description = "男性", OrderNo = "SC-GC-1", SystemCodeId = code.Id },
            new SystemCodeDetail { Code = "SC-GC-000002", Description = "女性", OrderNo = "SC-GC-2", SystemCodeId = code.Id }
        );
        appDbContext.SaveChanges();
    }


    private static void FeedSystemCodes(ApplicationDbContext appDbContext)
    {
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-GC", Description = "性别" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-PQ", Description = "专业职务" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-RP", Description = "聘用职务" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-QA", Description = "执业资格" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-PC", Description = "专业类别" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-JT", Description = "职称" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-LP", Description = "领导职务" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-GS", Description = "毕业院校" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-GM", Description = "毕业专业" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-PS", Description = "政治面貌" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-CS", Description = "身份" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-HS", Description = "最高学历" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-AS", Description = "在职状态" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-SA", Description = "系统状态" });
        appDbContext.SystemCodes.Add(new SystemCode { Code = "SC-NA", Description = "民族" });

        appDbContext.SaveChanges();
    }

    private static void FeedSystemSettings(ApplicationDbContext appDbContext)
    {
        #region  GET SYSTEM CODE
        var PQId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-PQ").Id;
        var RPId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-RP").Id;
        var QAId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-QA").Id;
        var PCId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-PC").Id;
        var JTId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-JT").Id;
        var LPId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-LP").Id;
        var GSId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-GS").Id;
        var GMId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-GM").Id;
        var PSId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-PS").Id;
        var CSId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-CS").Id;
        var HSId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-HS").Id;
        var ASId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-AS").Id;
        var SAId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-SA").Id;
        var NAId = appDbContext.SystemCodes.FirstOrDefault(r => r.Code == "SC-NA").Id;
        #endregion GET SYSTEM CODE
        appDbContext.SystemCodeDetails.AddRange(
        #region SC-PQ
            new SystemCodeDetail { SystemCodeId = PQId, Description = "中医士", Code = "SC-PQ-000001" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "主管护师", Code = "SC-PQ-000002" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "中医师", Code = "SC-PQ-000003" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "主治中医师", Code = "SC-PQ-000004" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "主治医师", Code = "SC-PQ-000005" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "副主任医师", Code = "SC-PQ-000006" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "医士", Code = "SC-PQ-000007" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "医师", Code = "SC-PQ-000008" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "药剂师", Code = "SC-PQ-000009" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "护师", Code = "SC-PQ-000010" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "检验师", Code = "SC-PQ-000011" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "药剂士", Code = "SC-PQ-000012" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "基层副主任医师", Code = "SC-PQ-000013" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "主管中药师", Code = "SC-PQ-000014" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "副主任护师", Code = "SC-PQ-000015" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "心理咨询师", Code = "SC-PQ-000016" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "基层副主任中医师", Code = "SC-PQ-000017" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "主管技师", Code = "SC-PQ-000018" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "基层主治中医师", Code = "SC-PQ-000019" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "主管药剂师", Code = "SC-PQ-000020" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "主管检验师", Code = "SC-PQ-000021" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "护士", Code = "SC-PQ-000022" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "基层主管护师", Code = "SC-PQ-000023" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "基层主治医师", Code = "SC-PQ-000024" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "康复治疗师", Code = "SC-PQ-000025" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "检验士", Code = "SC-PQ-000026" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "助理医师", Code = "SC-PQ-000027" },
            new SystemCodeDetail { SystemCodeId = PQId, Description = "主任中药师", Code = "SC-PQ-000028" },
        #endregion SC-PQ
        #region SC-RP
            new SystemCodeDetail { SystemCodeId = RPId, Description = "中医士", Code = "SC-RP-000001" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "主管护师", Code = "SC-RP-000002" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "中医师", Code = "SC-RP-000003" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "主治中医师", Code = "SC-RP-000004" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "主治医师", Code = "SC-RP-000005" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "副主治医师", Code = "SC-RP-000006" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "副主任医师", Code = "SC-RP-000007" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "医士", Code = "SC-RP-000008" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "医师", Code = "SC-RP-000009" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "药剂师", Code = "SC-RP-000010" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "护师", Code = "SC-RP-000011" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "检验师", Code = "SC-RP-000012" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "药剂士", Code = "SC-RP-000013" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "主管中药师", Code = "SC-RP-000014" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "副主任护师", Code = "SC-RP-000015" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "心理咨询师", Code = "SC-RP-000016" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "基层副主任中医师", Code = "SC-RP-000017" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "影像技师", Code = "SC-RP-000018" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "主管检验师", Code = "SC-RP-000019" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "护士", Code = "SC-RP-000020" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "康复治疗师", Code = "SC-RP-000021" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "助理医师", Code = "SC-RP-000022" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "副主任中药师", Code = "SC-RP-000023" },
            new SystemCodeDetail { SystemCodeId = RPId, Description = "副主任药师", Code = "SC-RP-000024" },
        #endregion SC-RP
        #region SC-QA
            new SystemCodeDetail { SystemCodeId = QAId, Description = "执业助理医师", Code = "SC-QA-000001" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "执业护士", Code = "SC-QA-000002" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "执业医师", Code = "SC-QA-000003" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "药剂师", Code = "SC-QA-000004" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "检验师", Code = "SC-QA-000005" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "药剂士", Code = "SC-QA-000006" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "执业药师", Code = "SC-QA-000007" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "心理咨询师", Code = "SC-QA-000008" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "中级技师", Code = "SC-QA-000009" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "检验师（师）", Code = "SC-QA-000010" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "主管药剂师", Code = "SC-QA-000011" },
            new SystemCodeDetail { SystemCodeId = QAId, Description = "康复技师", Code = "SC-QA-000012" },
        #endregion SC-QA
        #region SC-PC
            new SystemCodeDetail { SystemCodeId = PCId, Description = "中医", Code = "SC-PC-000001" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "护理", Code = "SC-PC-000002" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "临床", Code = "SC-PC-000003" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "药学", Code = "SC-PC-000004" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "检验", Code = "SC-PC-000005" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "心理学", Code = "SC-PC-000006" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "口腔", Code = "SC-PC-000007" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "影像", Code = "SC-PC-000008" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "公卫", Code = "SC-PC-000009" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "护理 ", Code = "SC-PC-000010" },
            new SystemCodeDetail { SystemCodeId = PCId, Description = "技师", Code = "SC-PC-000011" },
        #endregion SC-PC
        #region SC-JT
            new SystemCodeDetail { SystemCodeId = JTId, Description = "初级", Code = "SC-JT-000001" },
            new SystemCodeDetail { SystemCodeId = JTId, Description = "中级", Code = "SC-JT-000002" },
            new SystemCodeDetail { SystemCodeId = JTId, Description = "副高", Code = "SC-JT-000003" },
            new SystemCodeDetail { SystemCodeId = JTId, Description = "正高", Code = "SC-JT-000004" },
        #endregion SC-JT
        #region SC-LP
            new SystemCodeDetail { SystemCodeId = LPId, Description = "中层", Code = "SC-LP-000001" },
        #endregion SC-LP
        #region SC-GS
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川省成都卫生学校", Code = "SC-GS-000001" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都中医药大学", Code = "SC-GS-000002" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "泸州医学院", Code = "SC-GS-000003" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都市中医药大学", Code = "SC-GS-000004" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都电大", Code = "SC-GS-000005" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川省卫生干部学院", Code = "SC-GS-000006" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "国家开放大学", Code = "SC-GS-000007" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都医学院", Code = "SC-GS-000008" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川大学", Code = "SC-GS-000009" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都卫校", Code = "SC-GS-000010" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "西安交通大学", Code = "SC-GS-000011" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "川北医学院", Code = "SC-GS-000012" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "中国医科大学", Code = "SC-GS-000013" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "乐山师范学院", Code = "SC-GS-000014" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川中医药高等专科学校", Code = "SC-GS-000015" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "河北医科大学", Code = "SC-GS-000016" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "湖北省长江大学", Code = "SC-GS-000017" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "河北联合大学", Code = "SC-GS-000018" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川大学青羊校区", Code = "SC-GS-000019" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "山西医科大学", Code = "SC-GS-000020" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "西南医科大学", Code = "SC-GS-000021" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "山东力明科技职业学院", Code = "SC-GS-000022" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "遵义医学院", Code = "SC-GS-000023" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都体育学院", Code = "SC-GS-000024" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "佳木斯大学", Code = "SC-GS-000025" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "济宁医学院", Code = "SC-GS-000026" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "大连医科大学", Code = "SC-GS-000027" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "蚌埠医学院", Code = "SC-GS-000028" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "泰山医学院", Code = "SC-GS-000029" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "中南大学", Code = "SC-GS-000030" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川农业大学", Code = "SC-GS-000031" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "广元职工医学院", Code = "SC-GS-000032" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都中医学院", Code = "SC-GS-000033" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都中医药大学附属针灸学院(国家开放大学)", Code = "SC-GS-000034" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "贵洲省贵阳市贵医附院", Code = "SC-GS-000035" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "中央广播电视大学", Code = "SC-GS-000036" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都学院", Code = "SC-GS-000037" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "雅安职业技术学院", Code = "SC-GS-000038" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "达州职业技术学院", Code = "SC-GS-000039" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川广播电视大学", Code = "SC-GS-000040" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川省人民医院护理学校", Code = "SC-GS-000041" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "辽宁中医药大学杏林学院", Code = "SC-GS-000042" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "北京中医药大学", Code = "SC-GS-000043" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川大学附设华西卫生学校", Code = "SC-GS-000044" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "承德医学院", Code = "SC-GS-000045" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "南京医科大学", Code = "SC-GS-000046" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川工业科技学院", Code = "SC-GS-000047" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "长沙民政职业技术学院", Code = "SC-GS-000048" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川护理职业学院", Code = "SC-GS-000049" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "广西中医药大学赛恩斯新医药学院", Code = "SC-GS-000050" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "广安职业技术学院", Code = "SC-GS-000051" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "重庆三峡医药高等专科学校", Code = "SC-GS-000052" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "四川护理执业学院", Code = "SC-GS-000053" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "西南财经大学天府学院", Code = "SC-GS-000054" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "成都文理学院", Code = "SC-GS-000055" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "重庆医药高等专科学校", Code = "SC-GS-000056" },
            new SystemCodeDetail { SystemCodeId = GSId, Description = "皖南医学院", Code = "SC-GS-000057" },
        #endregion SC-GS
        #region SC-GM
            new SystemCodeDetail { SystemCodeId = GMId, Description = "中西医结合", Code = "SC-GM-000001" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "护理", Code = "SC-GM-000002" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "中医学", Code = "SC-GM-000003" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "临床医学", Code = "SC-GM-000004" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "高级护理", Code = "SC-GM-000005" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "行政管理", Code = "SC-GM-000006" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "中西医临床医学", Code = "SC-GM-000007" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "药学", Code = "SC-GM-000008" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "预防医学", Code = "SC-GM-000009" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "护理学", Code = "SC-GM-000010" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "药剂学", Code = "SC-GM-000011" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "中药学", Code = "SC-GM-000012" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "教育与心理学", Code = "SC-GM-000013" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "西医临床", Code = "SC-GM-000014" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "影像", Code = "SC-GM-000015" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "口腔医学", Code = "SC-GM-000016" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "医学影像", Code = "SC-GM-000017" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "医学检验", Code = "SC-GM-000018" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "中医学专业（中医骨伤科学方向）", Code = "SC-GM-000019" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "针灸推拿", Code = "SC-GM-000020" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "针灸学", Code = "SC-GM-000021" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "中西医结合临床", Code = "SC-GM-000022" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "妇产科学", Code = "SC-GM-000023" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "急诊医学", Code = "SC-GM-000024" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "医学影像学", Code = "SC-GM-000025" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "皮肤病与性病学", Code = "SC-GM-000026" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "麻醉学", Code = "SC-GM-000027" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "中西医临床", Code = "SC-GM-000028" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "人力资源管理", Code = "SC-GM-000029" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "临床", Code = "SC-GM-000030" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "中医学（中国医科大学药学技术药学）", Code = "SC-GM-000031" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = " 护理学", Code = "SC-GM-000032" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "针灸推拿学", Code = "SC-GM-000033" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "康复治疗学", Code = "SC-GM-000034" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "医学检验技术", Code = "SC-GM-000035" },
            new SystemCodeDetail { SystemCodeId = GMId, Description = "药剂", Code = "SC-GM-000036" },
        #endregion SC-GM
        #region SC-PS
            new SystemCodeDetail { SystemCodeId = PSId, Description = "中共党员", Code = "SC-PS-000001" },
            new SystemCodeDetail { SystemCodeId = PSId, Description = "团员", Code = "SC-PS-000002" },
            new SystemCodeDetail { SystemCodeId = PSId, Description = "共产党员", Code = "SC-PS-000003" },
            new SystemCodeDetail { SystemCodeId = PSId, Description = "民革", Code = "SC-PS-000004" },
            new SystemCodeDetail { SystemCodeId = PSId, Description = "预备党员", Code = "SC-PS-000005" },
            new SystemCodeDetail { SystemCodeId = PSId, Description = "党员", Code = "SC-PS-000006" },
        #endregion SC-PS
        #region SC-CS
            new SystemCodeDetail { SystemCodeId = CSId, Description = "干部", Code = "SC-CS-000001" },
            new SystemCodeDetail { SystemCodeId = CSId, Description = "工人", Code = "SC-CS-000002" },
            new SystemCodeDetail { SystemCodeId = CSId, Description = "其他", Code = "SC-CS-000003" },
        #endregion SC-CS
        #region SC-HS
            new SystemCodeDetail { SystemCodeId = HSId, Description = "中专", Code = "SC-HS-000001" },
            new SystemCodeDetail { SystemCodeId = HSId, Description = "大专(在职）", Code = "SC-HS-000002" },
            new SystemCodeDetail { SystemCodeId = HSId, Description = "本科(在职）", Code = "SC-HS-000003" },
            new SystemCodeDetail { SystemCodeId = HSId, Description = "大专", Code = "SC-HS-000004" },
            new SystemCodeDetail { SystemCodeId = HSId, Description = "本科", Code = "SC-HS-000005" },
            new SystemCodeDetail { SystemCodeId = HSId, Description = "硕士", Code = "SC-HS-000006" },
            new SystemCodeDetail { SystemCodeId = HSId, Description = "研究生（在职）", Code = "SC-HS-000007" },
        #endregion SC-HS
        #region SC-AS
            new SystemCodeDetail { SystemCodeId = ASId, Description = "正式-离职", Code = "SC-AS-000002" },
            new SystemCodeDetail { SystemCodeId = ASId, Description = "正式-内退", Code = "SC-AS-000003" },
            new SystemCodeDetail { SystemCodeId = ASId, Description = "正式-退休", Code = "SC-AS-000004" },
            new SystemCodeDetail { SystemCodeId = ASId, Description = "正式-在职", Code = "SC-AS-000001" },
            new SystemCodeDetail { SystemCodeId = ASId, Description = "正式-外派", Code = "SC-AS-000005" },
            new SystemCodeDetail { SystemCodeId = ASId, Description = "正式-挂职", Code = "SC-AS-000006" },
            new SystemCodeDetail { SystemCodeId = ASId, Description = "临时-在职", Code = "SC-AS-000007" },
            new SystemCodeDetail { SystemCodeId = ASId, Description = "临时-离职", Code = "SC-AS-000008" },
            new SystemCodeDetail { SystemCodeId = ASId, Description = "临时-借调", Code = "SC-AS-000009" },
            new SystemCodeDetail { SystemCodeId = ASId, Description = "临时-外派", Code = "SC-AS-000010" }
            #endregion SC-AS
            );
        appDbContext.SaveChanges();
    }
}