using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ManyToManyField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object entity;
            ICustomTypeDescriptor rowDescriptor = Row as ICustomTypeDescriptor;
            if (rowDescriptor != null)
            {
                // 从包装中获取实际实体
                entity = rowDescriptor.GetPropertyOwner(null);
            }
            else
            {
                entity = Row;
            }

            // 获取集合并确保加载它
            RelatedEnd entityCollection = Column.EntityTypeProperty.GetValue(entity, null) as RelatedEnd;
            if (entityCollection == null)
            {
                throw new InvalidOperationException(String.Format("ManyToMany 模板不支持 '{1}' 表上的 '{0}' 列的集合类型。", Column.Name, Table.Name));
            }
            if (!entityCollection.IsLoaded)
            {
                entityCollection.Load();
            }

            // 将 Repeater 绑定到子实体的列表中
            Repeater1.DataSource = entityCollection;
            Repeater1.DataBind();
        }

        public override Control DataControl
        {
            get
            {
                return Repeater1;
            }
        }

    }
}
