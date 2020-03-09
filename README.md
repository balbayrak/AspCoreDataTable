
# AspCoreDataTable

A htmlHelper for [jquery datatable](https://datatables.net/) . This helper provides server side ability with wrapping jquery datatable. Create a datatable without knowing jquery datatable usage.

* **Add AspCoreDataTable package to your project.**

```
 PM> Install-Package AspCoreDataTable.Core
```

## Dependencies

* **Server side**

Server side dependencies getting with AspCoreDataTable nuget package.
```
Microsoft.AspNetCore.Html.Abstractions
Microsoft.AspNetCore.Mvc
Microsoft.Extensions.DependencyInjection
```
* **Client side**

Add a link on datatables css and javascript files on your page
```
jquery
bootstrap
alertify,bootbox,sweetalert,toastr (These support by htmlHelper with confirm service)

```

## How to Use

After adding the AspCoreDataTable dll in your project, you can follow these steps for creating your own datatable.

* **Add a using on the library in the *_ViewImports.cshtml* file**

```csharp
@using AspCoreDataTable.Core.DataTable;
@using AspCoreDataTable.Core.Button.Concrete;
@using AspCoreDataTable.Core.General.Enums;
@using AspCoreDataTable.Core.ConfirmBuilder;
@using AspCoreDataTable.Core.DataTable.Toolbar;
@using AspCoreDataTable.Core.DataTable.Abstract;
@using AspCoreDataTable.Core.Button.Abstract;
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

* **Add required javascript files (datatable.js, alert.js, entity.js, general.js)**

You can get these javascript files in AspCoreDataTable.Test project assets folder.

* **Basic Initialization**

The easy way to use it for creating tables is to call the htmlHelper with your table id.
```csharp
     @(Html.DataTableHelper<Person>("persontable")
                ...
```

* **Add table load action your server side codes**

When dealing with thousands of data rows it can be helpfull to use the serverside configuration. You can use paging configuration for getting data.

```csharp

   @(Html.DataTableHelper<Person>("persontable")
            .LoadAction("/Home/LoadTable")
                    ...

 [HttpPost]
        public JsonResult LoadTable(JQueryDataTablesModel jQueryDataTablesModel)
        {
            try
            {
                return Json(jQueryDataTablesModel.ToJqueryDataTablesResponse(personList));
            }
            catch
            {
                // ignored
            }
            return null;
        }



```
* **Extension method for retrieving data**

```csharp
 public static JQueryDataTablesResponse ToJqueryDataTablesResponse<TEntity>(this JQueryDataTablesModel model, IEnumerable<TEntity> collection)
            where TEntity : class
        {
            List<TEntity> result = null;
            var storageObject = model.columnInfos.DeSerialize<TEntity>();
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                Expression<Func<TEntity, bool>> expression = storageObject.GetSearchExpression(model.sSearch.Trim());
                result = collection.AsQueryable().Where(expression).Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            }
            else
            {
                result = collection.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            }

            using (var parser = new DatatableParser<TEntity>(result, storageObject))
            {
                return parser.Parse(model, collection.Count(), result.Count);
            }
        }
```
* **Add table columns with using expression of entity properties**

Columns are defined regarding to properties of entity. Columns can be different types, *BoundColumn*, *CheckColumn*, *ActionColumn* 

```csharp
  @(Html.DataTableHelper<Person>("persontable")
            .LoadAction("/Home/LoadTable")
            .Columns(column =>
            {
                //add checkbox  as tablecolumn or table column header. Checkbox value select with expression. 
                column.CheckColumn(p => p.id).CheckAllEnabled();
                //add bound column with title. Column which selected with IsPrimaryKey method, is hidden automatically.
                column.BoundColumn(p => p.id).Title("ID").IsPrimaryKey(true);
                //Add searchable method for string property of entity. This search method can use by serverside action.
                column.BoundColumn(p => p.name).Title("Name").Searchable(Operation.StartsWith);
                //add column with nested property of entity.
                column.BoundColumn(p => p.PersonAdress.city).Title("City").Searchable(Operation.StartsWith);
                                        ...
```

You can create action columns with one action button or more. You can add *ActionButton*, *ModalButton*, *ConfirmButton*, *DownloadButton*
```csharp
 column.ActionColumn().Title("Actions").Actions(action =>
                {
                    action.ModalButton()
                        //You can hidden button regarding to propert value.
                        .Hidden(t => t.PersonAdress.city, "istanbul")
                        .Text("Edit")
                        .CssClass("btn btn-sm green btn-outline filter-submit margin-bottom green-haze")
                        .IClass("fa fa-edit")
                        .ActionInfo(new ActionInfo { actionUrl = "/Home/AddOrEdit", methodType = EnumHttpMethod.GET })
                        .BlockUI()
                        .Modal(EnumModalSize.Large)
                        .BackDropStatic();

                    action.ConfirmButton()
                        .Hidden(t => t.name, "Linda")
                        .Text("Delete")
                        .CssClass("btn btn-sm red btn-outline filter-submit margin-bottom red-haze")
                        .IClass("fa fa-trash-o")
                        .ActionInfo(new ActionInfo { actionUrl = "/Home/Delete", methodType = EnumHttpMethod.POST })
                        .BlockUI()
                        .ConfirmOption(new ConfirmOption("ConfirmMessage", "ConfirmTitle", ConfirmType.Sweet, ""));
                });
```

* **Add table toolbar action**

You can create toolbar button left side or right side of your table and enable or disable search, print, export options.
```csharp
.ToolBarActions(action =>
            {
                action.ModalActionButton()
                    .FormSide(EnumFormSide.LetfSide)
                    .Text("Add New Person")
                    .Modal(EnumModalSize.Default)
                    .BackDropStatic()
                    .CssClass("btn btn-sm red btn-outline filter-submit margin-bottom red-haze")
                    .IClass("fa fa-plus")
                    .ActionInfo(new ActionInfo { actionUrl = "/Home/AddOrEdit/-1", methodType = EnumHttpMethod.GET })
                    .BlockUI();

            }, new TableExportSetting("Tools", "btn red btn-outline", false, true, true, true, EnumFormSide.RightSide))
```

* **Add table rows css condition**

You can create condition for rows with using expression of property and css value.

Create css in your custom css file.

```css
.red {
    background-color: #f2dede;
}

.yellow {
    background-color: #FFFACD;
}

.blue {
    background-color: #87CEEB;
}
```
Use these css in your row condition.

```csharp
.RowCssConditions(condition =>
{
    condition.AddRowCss(p => p.status, 1, "red");
    condition.AddRowCss(p => p.status, 0, "yellow");
    condition.AddRowCss(p => p.status, -1, "blue");
})
```

* **Wrapping table with portlet**
```csharp
.Portlet("Person Table", System.Drawing.Color.Red, "fa fa-cogs")
```

* **Add `css` for table**
```csharp
.CssClass("table table-striped table-hover table-bordered  dataTable no-footer")
```

* **Add `statesave` option for table**
```csharp
.StateSave(true)
```
* **Add `pagingtype` option for table**
```csharp
.PagingType(EnumPagingType.bootstrap_number)
```

# Test Project
You can see all usage in *AspCoreDataTable.Test.csproj* project.

