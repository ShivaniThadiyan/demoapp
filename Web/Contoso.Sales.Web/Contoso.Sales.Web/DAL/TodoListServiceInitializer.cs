using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contoso.Sales.Web.DAL
{
    public class TodoListServiceInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TodoListServiceContext>
    {
    }
}