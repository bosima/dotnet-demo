using System.Data;

public class DmConnectionFactory
{
    static string sqlConnString = "Server=127.0.0.1; UserId=TESTDB; PWD=123456";
    public static IDbConnection GetConn()
    {
        return new DmConnection(sqlConnString);
    }
}

public class PersonDapperDAL : IPersonDAL
{
    public PersonDapperDAL()
    {
    }

    public PersonModel Get(int id)
    {
        string sql = "select Id,Name,City from Person where Id=:Id";
        return DmConnectionFactory.GetConn().QueryFirstOrDefault<PersonModel>(sql, new { Id = id });
    }

    public List<PersonModel> GetList()
    {
        string sql = "select Id,Name,City from Person";
        return DmConnectionFactory.GetConn().Query<PersonModel>(sql).ToList();
    }

    public int Add(PersonModel model)
    {
        string sql = "insert into Person(Name,City) Values(:Name,:City);Select @@IDENTITY";
        return DmConnectionFactory.GetConn().QuerySingle<int>(sql, model);
    }

    public bool Update(PersonModel model)
    {
        string sql = "update Person set City=:City where Id=:Id";
        int result = DmConnectionFactory.GetConn().Execute(sql, model);
        return result > 0;
    }

    public bool Delete(int id)
    {
        string sql = "delete from Person where Id=:Id";
        int result = DmConnectionFactory.GetConn().Execute(sql, new { Id = id });
        return result > 0;
    }
}