using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.DynamicData;
using System.Web.Routing;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {
        private static MetaModel s_defaultModel = new MetaModel();
        public static MetaModel DefaultModel
        {
            get
            {
                return s_defaultModel;
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            //                    重要: 数据模型注册 
            // 取消对此行的注释，为 ASP.NET Dynamic Data 注册 ADO.NET Entity Framework 模型。
            // 若要设置 ScaffoldAllTables = true，需符合以下条件，
            // 即确定希望数据模型中的所有表都支持基架(即模板)。若要控制各个表的
            // 基架，请为表创建分部类并将
            // [ScaffoldTable(true)] 特性应用于分部类。
            // 注意: 请确保将“YourDataContextType”更改为应用程序的数据上下文类的
            // 名称。
            //DefaultModel.RegisterContext(typeof(YourDataContextType), new ContextConfiguration() { ScaffoldAllTables = false });

            // 下面的语句支持分页模式，在这种模式下，“列表”、“详细”、“插入” 
            // 和“更新”任务是使用不同页执行的。若要启用此模式，请取消注释下面 
            // 的 route 定义，并注释掉后面的 combined-page 模式部分中的 route 定义。
            routes.Add(new DynamicDataRoute("{table}/{action}.aspx")
            {
                Constraints = new RouteValueDictionary(new { action = "List|Details|Edit|Insert" }),
                Model = DefaultModel
            });

            // 下面的语句支持 combined-page 模式，在这种模式下，“列表”、“详细”、“插入”
            // 和“更新”任务是使用同一页执行的。若要启用此模式，请取消注释下面
            // 的 routes，并注释掉上面的分页模式部分中的 route 定义。
            //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
            //    Action = PageAction.List,
            //    ViewName = "ListDetails",
            //    Model = DefaultModel
            //});

            //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
            //    Action = PageAction.Details,
            //    ViewName = "ListDetails",
            //    Model = DefaultModel
            //});
        }

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

    }
}
