using System;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;

namespace WebApplication1
{
    public partial class ManyToMany_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        public void Page_Load(object sender, EventArgs e)
        {
            // 注册 DataSource 的更新事件
            EntityDataSource ds = (EntityDataSource)this.FindDataSourceControl();

            // 此字段模板用于执行编辑和插入操作
            ds.Updating += new EventHandler<EntityDataSourceChangingEventArgs>(DataSource_UpdatingOrInserting);
            ds.Inserting += new EventHandler<EntityDataSourceChangingEventArgs>(DataSource_UpdatingOrInserting);
        }

        void DataSource_UpdatingOrInserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            MetaTable childTable = ChildrenColumn.ChildTable;

            // 注释以员工/区域为例来进行说明，但代码是泛型的

            // 获取此员工对应的区域的集合
            RelatedEnd entityCollection = (RelatedEnd)Column.EntityTypeProperty.GetValue(e.Entity, null);

            // 在编辑模式下，确保加载该集合(在插入模式下没有意义)
            if (Mode == DataBoundControlMode.Edit && !entityCollection.IsLoaded)
            {
                entityCollection.Load();
            }

            // 从该集合获取 IList (例如，当前员工对应的区域的列表)
            // REVIEW: 我们应直接使用 EntityCollection，但 EF 没有
            // 非泛型类型可用于它。将在 vnext 中添加相关类型
            IList entityList = ((IListSource)entityCollection).GetList();

            // 检查所有区域(而不仅仅是此员工对应的那些区域)
            foreach (object childEntity in childTable.GetQuery(e.Context))
            {

                // 检查该员工当前是否具有此区域
                bool isCurrentlyInList = entityList.Contains(childEntity);

                // 查找此区域对应的复选框，它会我们显示新状态
                string pkString = childTable.GetPrimaryKeyString(childEntity);
                ListItem listItem = CheckBoxList1.Items.FindByValue(pkString);
                if (listItem == null)
                    continue;

                // 如果状态不同，则进行适当的添加/移除更改
                if (listItem.Selected)
                {
                    if (!isCurrentlyInList)
                        entityList.Add(childEntity);
                }
                else
                {
                    if (isCurrentlyInList)
                        entityList.Remove(childEntity);
                }
            }
        }

        protected void CheckBoxList1_DataBound(object sender, EventArgs e)
        {
            MetaTable childTable = ChildrenColumn.ChildTable;

            // 注释以员工/区域为例来进行说明，但代码是泛型的

            IList entityList = null;
            ObjectContext objectContext = null;

            if (Mode == DataBoundControlMode.Edit)
            {
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

                // 获取此员工对应的区域集合并确保加载该集合
                RelatedEnd entityCollection = Column.EntityTypeProperty.GetValue(entity, null) as RelatedEnd;
                if (entityCollection == null)
                {
                    throw new InvalidOperationException(String.Format("ManyToMany 模板不支持 '{1}' 表上的 '{0}' 列的集合类型。", Column.Name, Table.Name));
                }
                if (!entityCollection.IsLoaded)
                {
                    entityCollection.Load();
                }

                // 从该集合获取 IList (例如，当前员工对应的区域的列表)
                // REVIEW: 我们应直接使用 EntityCollection，但 EF 没有
                // 非泛型类型可用于它。将在 vnext 中添加相关类型
                entityList = ((IListSource)entityCollection).GetList();

                // 获取当前的 ObjectContext
                // REVIEW: 这是用来执行此操作的相当低级的方法。请寻找更好的替代方法
                ObjectQuery objectQuery = (ObjectQuery)entityCollection.GetType().GetMethod(
                    "CreateSourceQuery").Invoke(entityCollection, null);
                objectContext = objectQuery.Context;
            }

            // 检查所有区域(而不仅仅是此员工对应的那些区域)
            foreach (object childEntity in childTable.GetQuery(objectContext))
            {
                MetaTable actualTable = MetaTable.GetTable(childEntity.GetType());
                // 为它创建复选框
                ListItem listItem = new ListItem(
                    actualTable.GetDisplayString(childEntity),
                    actualTable.GetPrimaryKeyString(childEntity));

                // 如果当前员工具有该区域，则选定它
                if (Mode == DataBoundControlMode.Edit)
                {
                    listItem.Selected = entityList.Contains(childEntity);
                }

                CheckBoxList1.Items.Add(listItem);
            }
        }

        public override Control DataControl
        {
            get
            {
                return CheckBoxList1;
            }
        }

    }
}
