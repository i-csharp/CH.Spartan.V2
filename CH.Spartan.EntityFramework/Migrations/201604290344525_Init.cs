namespace CH.Spartan.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250, storeType: "nvarchar"),
                        Points = c.String(unicode: false),
                        TenantId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Area_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Area_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpTenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Phone = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        TenancyName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        EditionId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.EditionId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Avatar = c.String(unicode: false),
                        IsStatic = c.Boolean(nullable: false),
                        QQOpenId = c.String(maxLength: 200, storeType: "nvarchar"),
                        QQAccessToken = c.String(maxLength: 200, storeType: "nvarchar"),
                        QQExpiresIn = c.Int(),
                        WeiXinUnionId = c.String(maxLength: 200, storeType: "nvarchar"),
                        WeiXinAccessToken = c.String(maxLength: 200, storeType: "nvarchar"),
                        WeiXinExpiresIn = c.Int(),
                        WeiBoOpenId = c.String(maxLength: 200, storeType: "nvarchar"),
                        WeiBoAccessToken = c.String(maxLength: 200, storeType: "nvarchar"),
                        WeiBoExpiresIn = c.Int(),
                        AuthenticationSource = c.String(maxLength: 64, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Surname = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        EmailAddress = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        EmailConfirmationCode = c.String(maxLength: 128, storeType: "nvarchar"),
                        PasswordResetCode = c.String(maxLength: 328, storeType: "nvarchar"),
                        LastLoginTime = c.DateTime(precision: 0),
                        IsActive = c.Boolean(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        TenantId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsGranted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                        UserId = c.Long(),
                        RoleId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AbpRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AbpUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Value = c.String(maxLength: 2000, storeType: "nvarchar"),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpEditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        DisplayName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpAuditLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        ServiceName = c.String(maxLength: 256, storeType: "nvarchar"),
                        MethodName = c.String(maxLength: 256, storeType: "nvarchar"),
                        Parameters = c.String(maxLength: 1024, storeType: "nvarchar"),
                        ExecutionTime = c.DateTime(nullable: false, precision: 0),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(maxLength: 64, storeType: "nvarchar"),
                        ClientName = c.String(maxLength: 128, storeType: "nvarchar"),
                        BrowserInfo = c.String(maxLength: 256, storeType: "nvarchar"),
                        Exception = c.String(maxLength: 2000, storeType: "nvarchar"),
                        ImpersonatorUserId = c.Long(),
                        ImpersonatorTenantId = c.Int(),
                        CustomData = c.String(maxLength: 2000, storeType: "nvarchar"),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpBackgroundJobs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        JobType = c.String(nullable: false, maxLength: 512, storeType: "nvarchar"),
                        JobArgs = c.String(nullable: false, unicode: false),
                        TryCount = c.Short(nullable: false),
                        NextTryTime = c.DateTime(nullable: false, precision: 0),
                        LastTryTime = c.DateTime(precision: 0),
                        IsAbandoned = c.Boolean(nullable: false),
                        Priority = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.IsAbandoned, t.NextTryTime });
            
            CreateTable(
                "dbo.DealRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        No = c.String(maxLength: 250, storeType: "nvarchar"),
                        OutNo = c.String(maxLength: 250, storeType: "nvarchar"),
                        Name = c.String(maxLength: 250, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 250, storeType: "nvarchar"),
                        Type = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsSucceed = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DealRecord_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DealRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        BNo = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        BSimNo = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        BDscription = c.String(maxLength: 50, storeType: "nvarchar"),
                        BIconType = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        BDeviceTypeId = c.Int(nullable: false),
                        BCode = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        BExpireTime = c.DateTime(precision: 0),
                        BNodeId = c.Int(nullable: false),
                        SLimitSpeed = c.Int(nullable: false),
                        SInOutArea = c.String(unicode: false),
                        GDirection = c.Int(nullable: false),
                        GHeight = c.Double(nullable: false),
                        GLatitude = c.Double(nullable: false),
                        GLongitude = c.Double(nullable: false),
                        GSpeed = c.Double(nullable: false),
                        GReportTime = c.DateTime(precision: 0),
                        GReceiveTime = c.DateTime(precision: 0),
                        GIsLocated = c.Boolean(nullable: false),
                        CIsAccOn = c.Boolean(nullable: false),
                        CPower = c.Int(nullable: false),
                        CIsRelay1Enable = c.Boolean(nullable: false),
                        CRelay1BreakTime = c.DateTime(precision: 0),
                        CIsRelay2Enable = c.Boolean(nullable: false),
                        CRelay2BreakTime = c.DateTime(precision: 0),
                        CIsRelay3Enable = c.Boolean(nullable: false),
                        CRelay3BreakTime = c.DateTime(precision: 0),
                        CIsMainPowerBreak = c.Boolean(nullable: false),
                        CMainPowerBreakTime = c.DateTime(precision: 0),
                        CMainPowerBreakLastAlarmTime = c.DateTime(precision: 0),
                        CIsSos = c.Boolean(nullable: false),
                        COverSpeedTime = c.DateTime(precision: 0),
                        COverSpeedLastAlarmTime = c.DateTime(precision: 0),
                        CStartupTime = c.DateTime(precision: 0),
                        CStartupLastAlarmTime = c.DateTime(precision: 0),
                        CLastHaveSpeedTime = c.DateTime(precision: 0),
                        CLastHaveSpeedLastAlarmTime = c.DateTime(precision: 0),
                        CInAreaList = c.String(maxLength: 500, storeType: "nvarchar"),
                        CIsFortify = c.Boolean(nullable: false),
                        CFortifyLatLng = c.String(maxLength: 100, storeType: "nvarchar"),
                        CLeaveFortifyAreaTime = c.DateTime(precision: 0),
                        CLeaveFortifyAreaLastAlarmTime = c.DateTime(precision: 0),
                        CIsDropOff = c.Boolean(nullable: false),
                        CDropOffTime = c.DateTime(precision: 0),
                        CDropOffLastAlarmTime = c.DateTime(precision: 0),
                        CIsShake = c.Boolean(nullable: false),
                        CShakeTime = c.DateTime(precision: 0),
                        CIsLowBattery = c.Boolean(nullable: false),
                        CLowBatteryTime = c.DateTime(precision: 0),
                        CLowBatteryLastAlarmTime = c.DateTime(precision: 0),
                        CIsInBlindArea = c.Boolean(nullable: false),
                        CInBlindAreaTime = c.DateTime(precision: 0),
                        CInBlindAreaLastAlarmTime = c.DateTime(precision: 0),
                        TenantId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Device_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Device_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceTypes", t => t.BDeviceTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Nodes", t => t.BNodeId, cascadeDelete: true)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BDeviceTypeId)
                .Index(t => t.BNodeId)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DeviceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        SwitchInGateway = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Supplier = c.String(maxLength: 150, storeType: "nvarchar"),
                        Manufacturer = c.String(maxLength: 150, storeType: "nvarchar"),
                        ServiceCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CodeCreateRule = c.Int(nullable: false),
                        IsHaveRelay1 = c.Boolean(nullable: false),
                        IsHaveRelay2 = c.Boolean(nullable: false),
                        IsHaveRelay3 = c.Boolean(nullable: false),
                        IsHavePower = c.Boolean(nullable: false),
                        IsHaveAcc = c.Boolean(nullable: false),
                        IsHaveOffLine = c.Boolean(nullable: false),
                        IsHaveMainPowerBreak = c.Boolean(nullable: false),
                        IsHaveSos = c.Boolean(nullable: false),
                        IsHaveOverSpeed = c.Boolean(nullable: false),
                        IsHaveInArea = c.Boolean(nullable: false),
                        IsHaveFortify = c.Boolean(nullable: false),
                        IsHaveDropOff = c.Boolean(nullable: false),
                        IsHaveShake = c.Boolean(nullable: false),
                        IsHaveLowBattery = c.Boolean(nullable: false),
                        IsHaveInBlindArea = c.Boolean(nullable: false),
                        IsHaveOutBlindArea = c.Boolean(nullable: false),
                        IsHaveStartup = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DeviceType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        HistoryTableName = c.String(maxLength: 100, storeType: "nvarchar"),
                        HistoryConnectionStringRead = c.String(maxLength: 250, storeType: "nvarchar"),
                        HistoryConnectionStringWrite = c.String(maxLength: 250, storeType: "nvarchar"),
                        UsageCount = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Node_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        No = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        TenantId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DeviceStock_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DeviceStock_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AbpFeatures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Value = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                        EditionId = c.Int(),
                        TenantId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.EditionId);
            
            CreateTable(
                "dbo.HistoryDatas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DeviceId = c.Int(nullable: false),
                        Direction = c.Int(nullable: false),
                        Height = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Speed = c.Double(nullable: false),
                        ReceiveTime = c.DateTime(nullable: false, precision: 0),
                        ReportTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        DisplayName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Icon = c.String(maxLength: 128, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpLanguageTexts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        LanguageName = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Source = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Key = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Value = c.String(nullable: false, unicode: false),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MileageReportDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        Day = c.DateTime(nullable: false, precision: 0),
                        TotalMileage = c.Double(nullable: false),
                        StartTime = c.DateTime(precision: 0),
                        StartLatLng = c.String(unicode: false),
                        StartAddress = c.String(unicode: false),
                        EndTime = c.DateTime(precision: 0),
                        EndLatLng = c.String(maxLength: 50, storeType: "nvarchar"),
                        EndAddress = c.String(maxLength: 50, storeType: "nvarchar"),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MileageReportDay_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NotificationName = c.String(nullable: false, maxLength: 96, storeType: "nvarchar"),
                        Data = c.String(unicode: false),
                        DataTypeName = c.String(maxLength: 512, storeType: "nvarchar"),
                        EntityTypeName = c.String(maxLength: 250, storeType: "nvarchar"),
                        EntityTypeAssemblyQualifiedName = c.String(maxLength: 512, storeType: "nvarchar"),
                        EntityId = c.String(maxLength: 96, storeType: "nvarchar"),
                        Severity = c.Byte(nullable: false),
                        UserIds = c.String(unicode: false),
                        ExcludedUserIds = c.String(unicode: false),
                        TenantIds = c.String(unicode: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpNotificationSubscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        NotificationName = c.String(maxLength: 96, storeType: "nvarchar"),
                        EntityTypeName = c.String(maxLength: 250, storeType: "nvarchar"),
                        EntityTypeAssemblyQualifiedName = c.String(maxLength: 512, storeType: "nvarchar"),
                        EntityId = c.String(maxLength: 96, storeType: "nvarchar"),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.NotificationName, t.EntityTypeName, t.EntityId, t.UserId });
            
            CreateTable(
                "dbo.AbpOrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        ParentId = c.Long(),
                        Code = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        DisplayName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpOrganizationUnits", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.AbpRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        IsStatic = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUserLoginAttempts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        TenancyName = c.String(maxLength: 64, storeType: "nvarchar"),
                        UserId = c.Long(),
                        UserNameOrEmailAddress = c.String(maxLength: 256, storeType: "nvarchar"),
                        ClientIpAddress = c.String(maxLength: 64, storeType: "nvarchar"),
                        ClientName = c.String(maxLength: 128, storeType: "nvarchar"),
                        BrowserInfo = c.String(maxLength: 256, storeType: "nvarchar"),
                        Result = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.TenancyName, t.UserNameOrEmailAddress, t.Result });
            
            CreateTable(
                "dbo.AbpUserNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Long(nullable: false),
                        NotificationId = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.UserId, t.State, t.CreationTime });
            
            CreateTable(
                "dbo.AbpUserOrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        OrganizationUnitId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbpRoles", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpPermissions", "RoleId", "dbo.AbpRoles");
            DropForeignKey("dbo.AbpRoles", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpOrganizationUnits", "ParentId", "dbo.AbpOrganizationUnits");
            DropForeignKey("dbo.AbpFeatures", "EditionId", "dbo.AbpEditions");
            DropForeignKey("dbo.DeviceStocks", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.Devices", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Devices", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.Devices", "BNodeId", "dbo.Nodes");
            DropForeignKey("dbo.Devices", "BDeviceTypeId", "dbo.DeviceTypes");
            DropForeignKey("dbo.DealRecords", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.DealRecords", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.Areas", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Areas", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpSettings", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpTenants", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "EditionId", "dbo.AbpEditions");
            DropForeignKey("dbo.AbpTenants", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpSettings", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserRoles", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpPermissions", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserLogins", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "CreatorUserId", "dbo.AbpUsers");
            DropIndex("dbo.AbpUserNotifications", new[] { "UserId", "State", "CreationTime" });
            DropIndex("dbo.AbpUserLoginAttempts", new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });
            DropIndex("dbo.AbpRoles", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpRoles", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpRoles", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpRoles", new[] { "TenantId" });
            DropIndex("dbo.AbpOrganizationUnits", new[] { "ParentId" });
            DropIndex("dbo.AbpNotificationSubscriptions", new[] { "NotificationName", "EntityTypeName", "EntityId", "UserId" });
            DropIndex("dbo.AbpFeatures", new[] { "EditionId" });
            DropIndex("dbo.DeviceStocks", new[] { "TenantId" });
            DropIndex("dbo.Devices", new[] { "UserId" });
            DropIndex("dbo.Devices", new[] { "TenantId" });
            DropIndex("dbo.Devices", new[] { "BNodeId" });
            DropIndex("dbo.Devices", new[] { "BDeviceTypeId" });
            DropIndex("dbo.DealRecords", new[] { "UserId" });
            DropIndex("dbo.DealRecords", new[] { "TenantId" });
            DropIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
            DropIndex("dbo.AbpSettings", new[] { "UserId" });
            DropIndex("dbo.AbpSettings", new[] { "TenantId" });
            DropIndex("dbo.AbpUserRoles", new[] { "UserId" });
            DropIndex("dbo.AbpPermissions", new[] { "RoleId" });
            DropIndex("dbo.AbpPermissions", new[] { "UserId" });
            DropIndex("dbo.AbpUserLogins", new[] { "UserId" });
            DropIndex("dbo.AbpUsers", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpUsers", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpUsers", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpUsers", new[] { "TenantId" });
            DropIndex("dbo.AbpTenants", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpTenants", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpTenants", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpTenants", new[] { "EditionId" });
            DropIndex("dbo.Areas", new[] { "UserId" });
            DropIndex("dbo.Areas", new[] { "TenantId" });
            DropTable("dbo.AbpUserOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserNotifications");
            DropTable("dbo.AbpUserLoginAttempts");
            DropTable("dbo.AbpRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpNotificationSubscriptions");
            DropTable("dbo.AbpNotifications");
            DropTable("dbo.MileageReportDays",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MileageReportDay_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpLanguageTexts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpLanguages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.HistoryDatas");
            DropTable("dbo.AbpFeatures");
            DropTable("dbo.DeviceStocks",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DeviceStock_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DeviceStock_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Nodes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Node_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.DeviceTypes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DeviceType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Devices",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Device_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Device_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.DealRecords",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DealRecord_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DealRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBackgroundJobs");
            DropTable("dbo.AbpAuditLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEditions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpSettings");
            DropTable("dbo.AbpUserRoles");
            DropTable("dbo.AbpPermissions");
            DropTable("dbo.AbpUserLogins");
            DropTable("dbo.AbpUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTenants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Areas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Area_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Area_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
