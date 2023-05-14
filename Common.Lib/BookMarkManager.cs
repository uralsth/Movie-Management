using SQLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Lib
{
    public class BookMarkManager
    {
        public async Task<OperationResponse<string>> AddOrRemove(string EntityId, string EntityType, string Username)
        {

            OperationResponse<string> response = new OperationResponse<string>();
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>()
            { new KeyValue("@EntityId", EntityId),
              new KeyValue("@EntityType", EntityType),
              new KeyValue("@Username",Username)
            };
            var status = await handlerAsync.ExecuteNonQueryAsync("[dbo].[usp_userBookmark_AddRemove]", param, "@OpStatus");
            if (status == 1)
                response.Result = "Added";
            else
                response.Result = "Removed";
            return response;
        }
        public async Task<IList<BookmarkItem>> GetMyBookmark(string EntityIds,string EntityType ,string Username)
        {
            SQLHandlerAsync handlerAsync = new SQLHandlerAsync();
            IList<KeyValue> param = new List<KeyValue>()
            { new KeyValue("@EntityIds", EntityIds),
              new KeyValue("@EntityType", EntityType),
              new KeyValue("@Username",Username)
            };
            return await handlerAsync.ExecuteAsListAsync<BookmarkItem>("[dbo].[usp_UserBookmark_Get]", param);
        }
    }
}
