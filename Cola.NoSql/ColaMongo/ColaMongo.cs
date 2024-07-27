using Cola.Models.Core.Models.ColaMongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cola.NoSql.ColaMongo;

public class ColaMongo : IColaMongo
{
    private readonly IMongoDatabase _database;

    /// <summary>
    /// MongoHelper
    /// </summary>
    /// <param name="mongoDbConfig">mongoDbConfig.</param>
    public ColaMongo(MongoDbConfig mongoDbConfig)
    {
        // 决定使用哪个库
        _database = new MongoClient(mongoDbConfig.ConnName).GetDatabase(mongoDbConfig.DatabaseName);
    }

    /// <summary>
    /// Gets 数据库对象
    /// </summary>
    public IMongoDatabase MongoDatabase => _database;

    /// <summary>
    /// ExistsCollection
    /// </summary>
    /// <param name="collName">collName</param>
    /// <typeparam name="T">T</typeparam>
    /// <returns>exists true,else false</returns>
    public bool ExistsCollection<T>(string collName)
    {
        return _database.GetCollection<T>(collName).AsQueryable().FirstOrDefault() != null;
    }

    /// <summary>
    /// ClearCollection
    /// </summary>
    /// <param name="collName">collName</param>
    /// <returns>bool</returns>
    public bool ClearCollection<T>(string collName)
    {
        this.DeleteMany(Builders<T>.Filter.Empty, collName);
        return true;
    }

    /// <summary>
    /// ClearCollectionAsync
    /// </summary>
    /// <param name="collName">collName</param>
    /// <returns>bool</returns>
    public async Task<bool> ClearCollectionAsync<T>(string collName)
    {
        await DeleteManyAsync(Builders<T>.Filter.Empty, collName);
        return true;
    }

    #region Add 添加一条数据

    /// <summary>
    /// 添加一条数据
    /// </summary>
    /// <param name="t">添加的实体</param>
    /// <param name="collName">_collName</param>
    /// <param name="T">T</param>
    /// <returns>bool</returns>
    public bool Add<T>(T t, string collName)
    {
        var client = _database.GetCollection<T>(collName);
        client.InsertOne(t);
        return true;
    }

    #endregion

    #region AddAsync 异步添加一条数据

    /// <summary>
    /// 异步添加一条数据
    /// </summary>
    /// <param name="t">添加的实体</param>
    /// <param name="collName">collName</param>
    /// <returns>true</returns>
    public async Task<bool> AddAsync<T>(T t, string collName)
    {
        var client = _database.GetCollection<T>(collName);
        await client.InsertOneAsync(t);
        return true;
    }

    #endregion

    #region InsertMany 批量插入

    /// <summary>
    /// 批量插入
    /// </summary>
    /// <param name="t">实体集合</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public bool InsertMany<T>(List<T> t, string collName)
    {
        var client = _database.GetCollection<T>(collName);
        client.InsertMany(t);
        return true;
    }

    #endregion

    #region InsertManyAsync 异步批量插入

    /// <summary>
    /// 异步批量插入
    /// </summary>
    /// <param name="t">实体集合</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public async Task<bool> InsertManyAsync<T>(List<T> t, string collName)
    {
        var client = _database.GetCollection<T>(collName);
        await client.InsertManyAsync(t);
        return true;
    }

    #endregion

    #region UpdateOne 修改数据(单条)

    /// <summary>
    /// 批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public UpdateResult UpdateOne<T>(
        UpdateDefinition<T> update,
        FilterDefinition<T> filter,
        string collName)
    {
        var client = this._database.GetCollection<T>(collName);
        return client.UpdateOne(filter, update);
    }

    #endregion

    #region UpdateOneAsync 异步批量修改数据

    /// <summary>
    /// 异步批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public async Task<UpdateResult> UpdateOneAsync<T>(
        UpdateDefinition<T> update,
        FilterDefinition<T> filter,
        string collName)
    {
        var client = this._database.GetCollection<T>(collName);
        return await client.UpdateOneAsync(filter, update);
    }

    #endregion

    #region UpdateManay 批量修改数据

    /// <summary>
    /// 批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public UpdateResult UpdateManay<T>(
        UpdateDefinition<T> update,
        FilterDefinition<T> filter,
        string collName)
    {
        var client = _database.GetCollection<T>(collName);
        return client.UpdateMany(filter, update);
    }

    #endregion

    #region UpdateManayAsync 异步批量修改数据

    /// <summary>
    /// 异步批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public async Task<UpdateResult> UpdateManayAsync<T>(
        UpdateDefinition<T> update,
        FilterDefinition<T> filter,
        string collName)
    {
        var client = _database.GetCollection<T>(collName);
        return await client.UpdateManyAsync(filter, update);
    }

    #endregion


    #region DeleteOne 删除一条数据

    /// <summary>
    /// 删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <returns></returns>
    public DeleteResult DeleteOne<T>(FilterDefinition<T> filter, string collName)
    {
        var client = _database.GetCollection<T>(collName);
        return client.DeleteOne(filter);
    }

    #endregion

    #region DeleteOneAsync 异步删除一条数据

    /// <summary>
    /// 异步删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public async Task<DeleteResult> DeleteOneAsync<T>(
        FilterDefinition<T> filter,
        string collName)
    {
        var client = _database.GetCollection<T>(collName);
        return await client.DeleteOneAsync(filter);
    }

    #endregion

    #region DeleteMany 删除多条数据

    /// <summary>
    /// 删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public DeleteResult DeleteMany<T>(FilterDefinition<T> filter, string collName)
    {
        var client = _database.GetCollection<T>(collName);
        return client.DeleteMany(filter);
    }

    #endregion

    #region DeleteManyAsync 异步删除多条数据

    /// <summary>
    /// 异步删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <param name="collName">collName</param>
    /// <returns>DeleteResult</returns>
    public async Task<DeleteResult> DeleteManyAsync<T>(
        FilterDefinition<T> filter,
        string collName)
    {
        var client = _database.GetCollection<T>(collName);
        return await client.DeleteManyAsync(filter);
    }

    #endregion

    #region 根据 filter 查询一条数据

    /// <summary>
    /// 根据id查询一条数据
    /// </summary>
    /// <param name="filter">要查询的字段，不写时查询全部</param>
    /// <param name="collName">collName</param>
    /// <param name="field">field</param>
    /// <returns>T</returns>
    public T? FindOne<T>(FilterDefinition<T> filter, string collName, string[]? field = null)
    {
        List<T>? result = FindList(filter, collName, field!);
        if (result != null && result.Count != 0 && result.Count > 1)
        {
            throw new Exception("查询数据超过多条");
        }

        return result!.Count == 0 ? default : result[0];
    }

    #endregion

    #region 根据 filter 查询一条数据

    /// <summary>
    /// 根据id查询一条数据
    /// </summary>
    /// <param name="filter">要查询的字段，不写时查询全部</param>
    /// <param name="collName">collName</param>
    /// <param name="field">field</param>
    /// <returns>T</returns>
    public async Task<T?> FindOneAsync<T>(
        FilterDefinition<T> filter,
        string collName,
        string[]? field = null)
    {
        var result = await this.FindListAsync(filter, collName, field);
        return result.SingleOrDefault();
    }

    #endregion

    #region FindOne 根据id查询一条数据

    /// <summary>
    /// 根据id查询一条数据
    /// </summary>
    /// <param name="id">objectid</param>
    /// <param name="collName">collName</param>
    /// <param name="isObjectId">isObjectId</param>
    /// <param name="field">field</param>
    /// <returns></returns>
    public T FindOne<T>(
        string id,
        string collName,
        bool isObjectId = true,
        string[]? field = null)
    {
        var client = this._database.GetCollection<T>(collName);
        FilterDefinition<T> filter;
        filter = isObjectId ? Builders<T>.Filter.Eq("_id", new ObjectId(id)) : Builders<T>.Filter.Eq("_id", id);

        // 不指定查询字段
        if (field == null || field.Length == 0)
        {
            return client.Find(filter).FirstOrDefault<T>();
        }

        // 制定查询字段
        var fieldList = field.Select(t => Builders<T>.Projection.Include(t.ToString())).ToList();

        var projection = Builders<T>.Projection.Combine(fieldList);
        fieldList?.Clear();
        return client.Find(filter).Project<T>(projection).FirstOrDefault<T>();
    }

    #endregion

    #region FindOneAsync 异步根据id查询一条数据

    /// <summary>
    /// FindOneAsync
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="collName">collName</param>
    /// <param name="isObjectId">isObjectId</param>
    /// <param name="field">field</param>
    /// <typeparam name="T">T</typeparam>
    /// <returns>Task T</returns>
    /// <exception cref="Exception">Exception</exception>
    public async Task<T> FindOneAsync<T>(
        string id,
        string collName,
        bool isObjectId = true,
        string[]? field = null)
    {
        var client = this._database.GetCollection<T>(collName);
        FilterDefinition<T> filter;
        filter = isObjectId ? Builders<T>.Filter.Eq("_id", new ObjectId(id)) : Builders<T>.Filter.Eq("_id", id);

        // 不指定查询字段
        if (field == null || field.Length == 0)
        {
            return await client.Find(filter).FirstOrDefaultAsync();
        }

        // 制定查询字段
        var fieldList = field.Select(t => Builders<T>.Projection.Include(t.ToString())).ToList();

        var projection = Builders<T>.Projection.Combine(fieldList);
        fieldList?.Clear();
        return await client.Find(filter).Project<T>(projection).FirstOrDefaultAsync();
    }

    #endregion

    #region FindList 查询集合

    /// <summary>
    /// 查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="collName">collName</param>
    /// <param name="field">field</param>
    /// <param name="sort">sort</param>
    /// <returns></returns>
    public List<T>? FindList<T>(
        FilterDefinition<T> filter,
        string collName,
        string[]? field = null,
        SortDefinition<T>? sort = null)
    {
        if (string.IsNullOrEmpty(collName))
            return null;
        var client = this._database.GetCollection<T>(collName);

        // 不指定查询字段
        if (field == null || field.Length == 0)
        {
            return sort == null ? client.FindAsync(filter).Result.ToList() :
                // 进行排序
                client.Find(filter).Sort(sort).ToList();
        }

        // 制定查询字段
        var fieldList = field.Select(t => Builders<T>.Projection.Include(t.ToString())).ToList();

        var projection = Builders<T>.Projection.Combine(fieldList);
        fieldList?.Clear();
        return sort == null ? client.Find(filter).Project<T>(projection).ToList() :
            // 排序查询
            client.Find(filter).Sort(sort).Project<T>(projection).ToList();
    }

    #endregion

    #region FindListAsync 异步查询集合

    /// <summary>
    /// 异步查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="collName">collName</param>
    /// <param name="field">field</param>
    /// <param name="sort">要排序的字段</param>
    /// <returns></returns>
    public async Task<List<T>> FindListAsync<T>(
        FilterDefinition<T> filter,
        string collName,
        string[]? field = null,
        SortDefinition<T>? sort = null)
    {
        var client = this._database.GetCollection<T>(collName);

        // 不指定查询字段
        if (field == null || field.Length == 0)
        {
            // return await client.Find(new BsonDocument()).ToListAsync();
            if (sort == null)
            {
                return await client.Find(filter).ToListAsync();
            }

            return await client.Find(filter).Sort(sort).ToListAsync();
        }

        // 制定查询字段
        var fieldList = field.Select(t => Builders<T>.Projection.Include(t.ToString())).ToList();

        var projection = Builders<T>.Projection.Combine(fieldList);
        fieldList.Clear();
        if (sort == null)
        {
            return await client.Find(filter).Project<T>(projection).ToListAsync();
        }

        // 排序查询
        return await client.Find(filter).Sort(sort).Project<T>(projection).ToListAsync();
    }

    #endregion

    #region FindListByPage 分页查询集合

    /// <summary>
    /// 分页查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">页容量</param>
    /// <param name="count">总条数</param>
    /// <param name="collName">collName</param>
    /// <param name="field">要查询的字段,不写时查询全部</param>
    /// <param name="sort">要排序的字段</param>
    /// <returns></returns>
    public List<T> FindListByPage<T>(
        FilterDefinition<T> filter,
        int pageIndex,
        int pageSize,
        out long count,
        string collName,
        string[]? field = null,
        SortDefinition<T>? sort = null)
    {
        var client = this._database.GetCollection<T>(collName);
        count = client.CountDocuments(filter);

        // 不指定查询字段
        if (field == null || field.Length == 0)
        {
            if (sort == null)
            {
                return client
                    .Find(filter)
                    .Skip((pageIndex - 1) * pageSize)
                    .Limit(pageSize)
                    .ToList();
            }

            // 进行排序
            return client
                .Find(filter)
                .Sort(sort)
                .Skip((pageIndex - 1) * pageSize)
                .Limit(pageSize)
                .ToList();
        }

        // 制定查询字段
        var fieldList = field.Select(t => Builders<T>.Projection.Include(t.ToString())).ToList();

        var projection = Builders<T>.Projection.Combine(fieldList);
        fieldList?.Clear();

        // 不排序
        if (sort == null)
        {
            return client
                .Find(filter)
                .Project<T>(projection)
                .Skip((pageIndex - 1) * pageSize)
                .Limit(pageSize)
                .ToList();
        }

        // 排序查询
        return client
            .Find(filter)
            .Sort(sort)
            .Project<T>(projection)
            .Skip((pageIndex - 1) * pageSize)
            .Limit(pageSize)
            .ToList();
    }

    #endregion

    #region FindListByPageAsync 异步分页查询集合

    /// <summary>
    /// 异步分页查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">页容量</param>
    /// <param name="collName">collName</param>
    /// <param name="field">要查询的字段,不写时查询全部</param>
    /// <param name="sort">要排序的字段</param>
    /// <returns></returns>
    public async Task<List<T>> FindListByPageAsync<T>(
        FilterDefinition<T> filter,
        int pageIndex,
        int pageSize,
        string collName,
        string[]? field = null,
        SortDefinition<T>? sort = null)
    {
        var client = this._database.GetCollection<T>(collName);

        // 不指定查询字段
        if (field == null || field.Length == 0)
        {
            if (sort == null)
            {
                return await client
                    .Find(filter)
                    .Skip((pageIndex - 1) * pageSize)
                    .Limit(pageSize)
                    .ToListAsync();
            }

            // 进行排序
            return await client
                .Find(filter)
                .Sort(sort)
                .Skip((pageIndex - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        // 制定查询字段
        var fieldList = field.Select(t => Builders<T>.Projection.Include(t.ToString())).ToList();

        var projection = Builders<T>.Projection.Combine(fieldList);
        fieldList?.Clear();

        // 不排序
        if (sort == null)
        {
            return await client
                .Find(filter)
                .Project<T>(projection)
                .Skip((pageIndex - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        // 排序查询
        return await client
            .Find(filter)
            .Sort(sort)
            .Project<T>(projection)
            .Skip((pageIndex - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    #endregion

    #region Count 根据条件获取总数

    /// <summary>
    /// 根据条件获取总数
    /// </summary>
    /// <param name="filter">条件</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public long Count<T>(FilterDefinition<T> filter, string collName)
    {
        var client = _database.GetCollection<T>(collName);
        return client.CountDocuments(filter);
    }

    #endregion

    #region CountAsync 异步根据条件获取总数

    /// <summary>
    /// 异步根据条件获取总数
    /// </summary>
    /// <param name="filter">条件</param>
    /// <param name="collName">collName</param>
    /// <returns></returns>
    public async Task<long> CountAsync<T>(FilterDefinition<T> filter, string collName)
    {
        var client = _database.GetCollection<T>(collName);
        return await client.CountDocumentsAsync(filter);
    }

    #endregion

    /// <summary>
    /// DeleteCollections
    /// </summary>
    /// <param name="collectionNames">drop collectionNames</param>
    /// <param name="notDrop">not Drop collectionNames</param>
    public void DeleteCollections(
        List<string>? collectionNames = null,
        List<string>? notDrop = null)
    {
        var lst =
            collectionNames
            ?? _database.ListCollections()
                .ToList()
                .Select(c => c["name"].ToString())
                .ToList()!;

        foreach (var collection in lst)
        {
            if (notDrop != null)
            {
                if (!notDrop.Contains(collection))
                {
                    _database.DropCollection(collection);
                }
            }
            else
            {
                _database.DropCollection(collection);
            }
        }
    }
}
