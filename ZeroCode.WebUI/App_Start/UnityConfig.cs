
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Web;

namespace ZeroCode.WebUI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = BuildContainerByConfig();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        /// <summary>
        /// ͨ�������ļ�����container
        /// </summary>
        /// <returns></returns>
        private static UnityContainer BuildContainerByConfig()
        {
            var container = new UnityContainer();
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = HttpContext.Current.Server.MapPath("~/Unity.config") };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection unitySection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

            //ʹ�ô���ע����������
            //container.RegisterType<IBaseRepository<SysSample, string>, BaseRepository<SysSample, string>>("SysRep1");
            //var te = typeof(IBaseRepository<SysSample, string>);
            //string str = te.AssemblyQualifiedName;

            //var te2 = typeof(BaseRepository<SysSample, string>);
            //string str2 = te2.AssemblyQualifiedName;

            container.LoadConfiguration(unitySection, "defaultContainer");
            return container;
        }

        /// <summary>
        /// Ӳ���뷽ʽ����container
        /// </summary>
        /// <returns></returns>
        private static UnityContainer BuildContainerByCode()
        {
             var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();


            
            return container;
        }
    }
}