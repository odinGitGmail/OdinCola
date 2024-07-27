### ColaMongo

#### 1. 配置文件配置

```json
"ColaMongo": {
    "ConnName": "mongodb://mongoUri",
    "DatabaseName": "dbName"
}
```

#### 2. 注入方式

```csharp
//通过配置文件注入
builder.Services.AddSingletonColaMongo(config);
// 直接注入
builder.Services.AddSingletonColaMongo(option =>
{
    option.ConnName = "mongodb://mongoUri";
    option.DatabaseName = "dbName";
});
```

#### 3. 使用方式

|接口名|接口含义|
|:--|:--|
|bool ExistsCollection<T>(string collName);|ExistsCollection|
|bool ClearCollection<T>(string collName);|ClearCollection|
|Task<bool> ClearCollectionAsync<T>(string collName);|ClearCollectionAsync|
|bool Add<T>(T t, string collName);|添加一条数据|
|Task<bool> AddAsync<T>(T t, string collName);|添加一条数据|
|bool InsertMany<T>(List<T> t, string collName);|异步添加一条数据|
|Task<bool> InsertManyAsync<T>(List<T> t, string collName);|批量插入|
|UpdateResult UpdateOne<T>(UpdateDefinition<T> update,FilterDefinition<T> filter,string collName);|批量修改数据|
|Task<UpdateResult> UpdateOneAsync<T>(UpdateDefinition<T> update,FilterDefinition<T> filter,string collName);|异步批量修改数据|
|UpdateResult UpdateManay<T>(UpdateDefinition<T> update,FilterDefinition<T> filter,string collName);|批量修改数据|
|Task<UpdateResult> UpdateManayAsync<T>(UpdateDefinition<T> update,FilterDefinition<T> filter,string collName);|异步批量修改数据|
|DeleteResult DeleteOne<T>(FilterDefinition<T> filter, string collName);|删除多条数据|
|Task<DeleteResult> DeleteOneAsync<T>(FilterDefinition<T> filter, string collName);|异步删除多条数据|
|DeleteResult DeleteMany<T>(FilterDefinition<T> filter, string collName);|删除多条数据|
|Task<DeleteResult> DeleteManyAsync<T>(FilterDefinition<T> filter, string collName);|异步删除多条数据|
|T FindOne<T>(FilterDefinition<T> filter, string collName, string[]? field = null);|根据id查询一条数据|
|Task<T> FindOneAsync<T>(FilterDefinition<T> filter, string collName, string[]? field = null);|根据id查询一条数据|
|T FindOne<T>(string id, string collName, bool isObjectId = true, string[]? field = null);|根据id查询一条数据|
|Task<T> FindOneAsync<T>(string id,string collName,bool isObjectId = true,string[]? field = null);|异步根据id查询一条数据|
|List<T> FindList<T>(FilterDefinition<T> filter,string collName,string[]? field = null,SortDefinition<T>? sort = null);|查询集合|
|Task<List<T>> FindListAsync<T>(FilterDefinition<T> filter,string collName,string[]? field = null,SortDefinition<T>? sort = null);|异步查询集合|
|List<T> FindListByPage<T>(FilterDefinition<T> filter,int pageIndex,int pageSize,out long count,string collName,string[]? field = null,SortDefinition<T>? sort = null);|分页查询集合|
|Task<List<T>> FindListByPageAsync<T>(FilterDefinition<T> filter,int pageIndex,int pageSize,string collName,string[]? field = null,SortDefinition<T>? sort = null);|异步分页查询集合|
|long Count<T>(FilterDefinition<T> filter, string collName);|根据条件获取总数|
|Task<long> CountAsync<T>(FilterDefinition<T> filter, string collName);|异步根据条件获取总数|
|void DeleteCollections(List<string>? collectionNames = null, List<string>? notDrop = null);|DeleteCollections|