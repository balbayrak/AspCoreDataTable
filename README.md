
# AspCoreDataTable

A htmlHelper for jquery datatable. This helper provides server side ability with wrapping jquery datatable. Create a datatable without knowing jquery datatable usage.

* **Add AspCoreDataTable package to your project.**

```
 PM> Install-Package AspCoreDataTable
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

* **Add a using on the library in the *_ViewImports.cshtml* file.**

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

* **Add table load action your server side codes.**

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
Columns are defined regarding to properties of entity.
```csharp

```


