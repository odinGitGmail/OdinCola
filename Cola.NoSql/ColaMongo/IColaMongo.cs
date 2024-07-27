using MongoDB.Driver;

namespace Cola.NoSql.ColaMongo;

public interface IColaMongo
{
    /// <summary>
    /// Gets 数据库对象
    /// </summary>
    IMongoDatabase MongoDatabase { get; }

    /// <summary>
    /// ExistsCollection
    /// </summary>
    /// <param name="collName">collName</param>
    /// <typeparam name="T">T</typeparam>
    /// <returns>exists true,else false</returns>
    bool ExistsCollection<T>(string collName);

    /// <summary>
    /// ClearCollection
    /// </summary>
    /// <param name="collName">collName</param>
    /// <returns>bool</returns>
    bool ClearCollection<T>(string collName);

    /// <summary>
    /// ClearCollectionAsync
    /// </summary>
    /// <param name="collName">collName</param>
    /// <returns>bool</returns>
    Task<bool> ClearCollectionAsync<T>(string collName);

    /// <summary>
    /// 添加一条数据
    /// </summary>
    /// <param name="t">添加的实体</param>
    /// <param name="collName">_collName</param>
    /// <param name="T">T</param>
    /// <returns>bool</returns>
    bool Add<T>(T t, string collName);

    /// <summary>
    /// 异步添加一条数据
    /// </summary>
    /// <param name="t">添加的实体</param>
    /// <returns>bool</returns>
    Task<bool> AddAsync<T>(T t, string collName);

    /// <summary>
    /// 批量插入
    /// </summary>
    /// <param name="t">实体集合</param>
    /// <returns>bool</returns>
    bool InsertMany<T>(List<T> t, string collName);

    /// <summary>
    /// 异步批量插入
    /// </summary>
    /// <param name="t">实体集合</param>
    /// <returns>bool</returns>
    Task<bool> InsertManyAsync<T>(List<T> t, string collName);

    /// <summary>
    /// 批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <returns>UpdateResult</returns>
    UpdateResult UpdateOne<T>(
        UpdateDefinition<T> update,
        FilterDefinition<T> filter,
        string collName);

    /// <summary>
    /// 异步批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <returns>UpdateResult</returns>
    Task<UpdateResult> UpdateOneAsync<T>(
        UpdateDefinition<T> update,
        FilterDefinition<T> filter,
        string collName);

    /// <summary>
    /// 批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <returns>UpdateResult</returns>
    UpdateResult UpdateManay<T>(
        UpdateDefinition<T> update,
        FilterDefinition<T> filter,
        string collName);

    /// <summary>
    /// 异步批量修改数据
    /// </summary>
    /// <param name="update">要修改的字段</param>
    /// <param name="filter">修改条件</param>
    /// <returns>UpdateResult</returns>
    Task<UpdateResult> UpdateManayAsync<T>(
        UpdateDefinition<T> update,
        FilterDefinition<T> filter,
        string collName);

    /// <summary>
    /// 删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <returns>DeleteResult</returns>
    DeleteResult DeleteOne<T>(FilterDefinition<T> filter, string collName);

    /// <summary>
    /// 异步删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <returns>DeleteResult</returns>
    Task<DeleteResult> DeleteOneAsync<T>(FilterDefinition<T> filter, string collName);

    /// <summary>
    /// 删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <returns>DeleteResult</returns>
    DeleteResult DeleteMany<T>(FilterDefinition<T> filter, string collName);

    /// <summary>
    /// 异步删除多条数据
    /// </summary>
    /// <param name="filter">删除的条件</param>
    /// <returns>DeleteResult</returns>
    Task<DeleteResult> DeleteManyAsync<T>(FilterDefinition<T> filter, string collName);

    /// <summary>
    /// 根据id查询一条数据
    /// </summary>
    /// <param name="filter">要查询的字段，不写时查询全部</param>
    /// <param name="collName">collName</param>
    /// <param name="field">field</param>
    /// <returns>T</returns>
    T? FindOne<T>(FilterDefinition<T> filter, string collName, string[]? field = null);

    /// <summary>
    /// 根据id查询一条数据
    /// </summary>
    /// <param name="filter">要查询的字段，不写时查询全部</param>
    /// <param name="collName">collName</param>
    /// <param name="field">field</param>
    /// <returns>T</returns>
    Task<T?> FindOneAsync<T>(FilterDefinition<T> filter, string collName, string[]? field = null);

    /// <summary>
    /// 根据id查询一条数据
    /// </summary>
    /// <param name="id">objectid</param>
    /// <param name="field">要查询的字段，不写时查询全部</param>
    /// <returns>T</returns>
    T FindOne<T>(string id, string collName, bool isObjectId = true, string[]? field = null);

    /// <summary>
    /// 异步根据id查询一条数据
    /// </summary>
    /// <param name="id">objectid</param>
    /// <returns>T</returns>
    Task<T> FindOneAsync<T>(
        string id,
        string collName,
        bool isObjectId = true,
        string[]? field = null);

    /// <summary>
    /// 查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="field">要查询的字段,不写时查询全部</param>
    /// <param name="sort">要排序的字段</param>
    /// <returns>List T</returns>
    List<T>? FindList<T>(
        FilterDefinition<T> filter,
        string collName,
        string[]? field = null,
        SortDefinition<T>? sort = null);

    /// <summary>
    /// 异步查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="field">要查询的字段,不写时查询全部</param>
    /// <param name="sort">要排序的字段</param>
    /// <returns>List T</returns>
    Task<List<T>> FindListAsync<T>(
        FilterDefinition<T> filter,
        string collName,
        string[]? field = null,
        SortDefinition<T>? sort = null);

    /// <summary>
    /// 分页查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">页容量</param>
    /// <param name="count">总条数</param>
    /// <param name="field">要查询的字段,不写时查询全部</param>
    /// <param name="sort">要排序的字段</param>
    /// <returns>List T</returns>
    List<T> FindListByPage<T>(
        FilterDefinition<T> filter,
        int pageIndex,
        int pageSize,
        out long count,
        string collName,
        string[]? field = null,
        SortDefinition<T>? sort = null);

    /// <summary>
    /// 异步分页查询集合
    /// </summary>
    /// <param name="filter">查询条件</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">页容量</param>
    /// <param name="field">要查询的字段,不写时查询全部</param>
    /// <param name="sort">要排序的字段</param>
    /// <returns>List T</returns>
    Task<List<T>> FindListByPageAsync<T>(
        FilterDefinition<T> filter,
        int pageIndex,
        int pageSize,
        string collName,
        string[]? field = null,
        SortDefinition<T>? sort = null);

    /// <summary>
    /// 根据条件获取总数
    /// </summary>
    /// <param name="filter">条件</param>
    /// <returns>long</returns>
    long Count<T>(FilterDefinition<T> filter, string collName);

    /// <summary>
    /// 异步根据条件获取总数
    /// </summary>
    /// <param name="filter">条件</param>
    /// <returns>long</returns>
    Task<long> CountAsync<T>(FilterDefinition<T> filter, string collName);

    /// <summary>
    /// DeleteCollections
    /// </summary>
    /// <param name="collectionNames">drop collectionNames</param>
    /// <param name="notDrop">not Drop collectionNames</param>
    void DeleteCollections(List<string>? collectionNames = null, List<string>? notDrop = null);
}
