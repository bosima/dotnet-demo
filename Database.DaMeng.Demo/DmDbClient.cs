public class DmDbHelper
    {
        private string connectionString = string.Empty;

        /// <summary>
        /// 初始化DMClient的一个新实例
        /// </summary>
        /// <param name="str"></param>
        public DmDbHelper(string str)
        {
            connectionString = str;
        }

        #region 通用快捷方法
        /// <summary>
        /// 执行一条SQL语句，确定记录是否存在
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        public bool Exists(string sql)
        {
            object obj = GetSingle(sql);

            int cmdresult;
            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            return cmdresult > 0;
        }

        /// <summary>
        /// 执行一条SQL语句，确定记录是否存在
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string sql)
        {
            object obj = await GetSingleAsync(sql);

            int cmdresult;
            if (Equals(obj, null) || Equals(obj, DBNull.Value))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            return cmdresult > 0;
        }

        /// <summary>
        /// 执行一条SQL语句，确定记录是否存在
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="paras">SQL参数数组</param>
        /// <returns></returns>
        public bool Exists(string sql, params DmParameter[] paras)
        {
            object obj = GetSingle(sql, paras);

            int cmdresult;
            if ((object.Equals(obj, null)) || (object.Equals(obj, DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            return cmdresult > 0;
        }

        /// <summary>
        /// 执行一条SQL语句，确定记录是否存在
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="paras">SQL参数数组</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string sql, params DmParameter[] paras)
        {
            object obj = await GetSingleAsync(sql, paras);

            int cmdresult;
            if ((object.Equals(obj, null)) || (object.Equals(obj, DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            return cmdresult > 0;
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="sqlCondition">查询条件</param>
        /// <returns></returns>
        public int GetCount(string tableName, string sqlCondition)
        {
            string sql = "select count(1) from `" + tableName + "`";

            if (!string.IsNullOrWhiteSpace(sqlCondition))
            {
                sql += " where " + sqlCondition;
            }

            object result = GetSingle(sql);

            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="sqlCondition">查询条件</param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(string tableName, string sqlCondition)
        {
            string sql = "select count(1) from `" + tableName + "`";

            if (!string.IsNullOrWhiteSpace(sqlCondition))
            {
                sql += " where " + sqlCondition;
            }

            object result = await GetSingleAsync(sql);

            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="sqlCondition">查询条件</param>
        /// <param name="paras">SQL参数数组</param>
        /// <returns></returns>
        public int GetCount(string tableName, string sqlCondition, DmParameter[] paras)
        {
            string sql = "select count(1) from `" + tableName + "`";

            if (!string.IsNullOrWhiteSpace(sqlCondition))
            {
                sql += " where " + sqlCondition;
            }

            object result = GetSingle(sql, paras);

            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="sqlCondition">查询条件</param>
        /// <param name="paras">SQL参数数组</param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(string tableName, string sqlCondition, DmParameter[] paras)
        {
            string sql = "select count(1) from `" + tableName + "`";

            if (!string.IsNullOrWhiteSpace(sqlCondition))
            {
                sql += " where " + sqlCondition;
            }

            object result = await GetSingleAsync(sql, paras);

            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return 0;
            }
        }

        #endregion 通用快捷方法

        #region 执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sql)
        {
            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand(sql, connection))
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public async Task<int> ExecuteSqlAsync(string sql)
        {
            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand(sql, connection))
                {
                    await connection.OpenAsync();
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数（可自定义超时时间）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="timeout">执行超时时间</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlByTime(string sql, int timeout)
        {
            using (DmConnection connection = new DmConnection(this.connectionString))
            {
                using (DmCommand cmd = new DmCommand(sql, connection))
                {
                    connection.Open();
                    cmd.CommandTimeout = timeout;
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数（可自定义超时时间）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="timeout">执行超时时间</param>
        /// <returns>影响的记录数</returns>
        public async Task<int> ExecuteSqlByTimeAsync(string sql, int timeout)
        {
            using (DmConnection connection = new DmConnection(this.connectionString))
            {
                using (DmCommand cmd = new DmCommand(sql, connection))
                {
                    await connection.OpenAsync();
                    cmd.CommandTimeout = timeout;
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows;
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlList">多条SQL语句</param>
        public void ExecuteSqlTrans(ArrayList sqlList)
        {
            using (DmConnection conn = new DmConnection(connectionString))
            {
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    using (DmCommand cmd = new DmCommand())
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = trans;

                        try
                        {
                            for (int n = 0; n < sqlList.Count; n++)
                            {
                                string sql = sqlList[n].ToString();

                                if (sql.Trim().Length > 1)
                                {
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            trans.Commit();
                        }
                        catch (DmException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlList">多条SQL语句</param>
        public async Task ExecuteSqlTransAsync(ArrayList sqlList)
        {
            using (DmConnection conn = new DmConnection(connectionString))
            {
                await conn.OpenAsync();
                using (DbTransaction trans = await conn.BeginTransactionAsync())
                {
                    using (DmCommand cmd = new DmCommand())
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = trans;

                        try
                        {
                            for (int n = 0; n < sqlList.Count; n++)
                            {
                                string sql = sqlList[n].ToString();

                                if (sql.Trim().Length > 1)
                                {
                                    cmd.CommandText = sql;
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            trans.Commit();
                        }
                        catch (DmException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条SQL查询语句，返回查询结果。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns>查询结果</returns>
        public object GetSingle(string sql)
        {
            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand(sql, connection))
                {
                    connection.Open();

                    object obj = cmd.ExecuteScalar();

                    if ((object.Equals(obj, null)) || (object.Equals(obj, DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条SQL查询语句，返回查询结果。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns>查询结果</returns>
        public async Task<object> GetSingleAsync(string sql)
        {
            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand(sql, connection))
                {
                    await connection.OpenAsync();

                    object obj = await cmd.ExecuteScalarAsync();

                    if ((object.Equals(obj, null)) || (object.Equals(obj, DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DbDataReader（切记要手工关闭DbDataReader）
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DmDataReader</returns>
        public DbDataReader ExecuteReader(string sql)
        {
            DmConnection connection = new DmConnection(connectionString);
            DmCommand cmd = new DmCommand(sql, connection);

            connection.Open();
            return cmd.ExecuteReader();
        }

        /// <summary>
        /// 执行查询语句，返回DbDataReader（切记要手工关闭DbDataReader）
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DmDataReader</returns>
        public async Task<DbDataReader> ExecuteReaderAsync(string sql)
        {
            DmConnection connection = new DmConnection(connectionString);
            DmCommand cmd = new DmCommand(sql, connection);

            await connection.OpenAsync();
            return await cmd.ExecuteReaderAsync();
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sql)
        {
            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmDataAdapter command = new DmDataAdapter(sql, connection))
                {
                    DataSet ds = new DataSet();

                    connection.Open();
                    command.Fill(ds, "ds");

                    return ds;
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet（可自定义超时时间）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public DataSet Query(string sql, int timeout)
        {
            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmDataAdapter command = new DmDataAdapter(sql, connection))
                {
                    DataSet ds = new DataSet();

                    connection.Open();
                    command.SelectCommand.CommandTimeout = timeout;
                    command.Fill(ds, "ds");

                    return ds;
                }
            }
        }
        #endregion 执行简单SQL语句

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paras">SQL参数数组</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sql, params DmParameter[] paras)
        {
            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand())
                {
                    PrepareCommand(cmd, connection, null, sql, paras);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paras">SQL参数数组</param>
        /// <returns>影响的记录数</returns>
        public async Task<int> ExecuteSqlAsync(string sql, params DmParameter[] paras)
        {
            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand())
                {
                    await PrepareCommandAsync(cmd, connection, null, sql, paras);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    cmd.Parameters.Clear();
                    return rows;
                }
            }
        }

        /// <summary>
        /// 执行添加SQL语句，返回记录的ID（自动产生的自增主键）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parms">SQL参数</param>
        /// <returns>记录的ID</returns>
        public int ExecuteAdd(string sql, params DmParameter[] parms)
        {
            sql = sql + ";Select @@IDENTITY";

            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand())
                {
                    PrepareCommand(cmd, connection, null, sql, parms);
                    int recordID = Int32.Parse(cmd.ExecuteScalar().ToString());
                    cmd.Parameters.Clear();

                    return recordID;
                }
            }
        }

        /// <summary>
        /// 执行添加SQL语句，返回记录的ID（自动产生的自增主键）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parms">SQL参数</param>
        /// <returns>记录的ID</returns>
        public async Task<int> ExecuteAddAsync(string sql, params DmParameter[] parms)
        {
            sql = sql + ";select @@identity as newautoid";

            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand())
                {
                    await PrepareCommandAsync(cmd, connection, null, sql, parms);

                    int recordID;
                    try
                    {
                        recordID = int.Parse((await cmd.ExecuteScalarAsync()).ToString());
                    }
                    catch
                    {
                        recordID = -1;
                    }

                    cmd.Parameters.Clear();

                    return recordID;
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlList">SQL语句的哈希表（key为sql语句，value是该语句的DmParameter[]）</param>
        public void ExecuteSqlTrans(Hashtable sqlList)
        {
            using (DmConnection conn = new DmConnection(connectionString))
            {
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    using (DmCommand cmd = new DmCommand())
                    {
                        try
                        {
                            foreach (DictionaryEntry entry in sqlList)
                            {
                                var sql = entry.Key.ToString();
                                var paras = (DmParameter[])entry.Value;

                                PrepareCommand(cmd, conn, trans, sql, paras);

                                int val = cmd.ExecuteNonQuery();

                                cmd.Parameters.Clear();
                            }

                            trans.Commit();
                        }
                        catch (DmException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlList">SQL语句的哈希表（key为sql语句，value是该语句的DmParameter[]）</param>
        public async Task ExecuteSqlTransAsync(Hashtable sqlList)
        {
            using (DmConnection conn = new DmConnection(connectionString))
            {
                await conn.OpenAsync();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    using (DmCommand cmd = new DmCommand())
                    {
                        try
                        {
                            foreach (DictionaryEntry entry in sqlList)
                            {
                                var sql = entry.Key.ToString();
                                var paras = (DmParameter[])entry.Value;

                                await PrepareCommandAsync(cmd, conn, trans, sql, paras);

                                int val = await cmd.ExecuteNonQueryAsync();

                                cmd.Parameters.Clear();
                            }

                            trans.Commit();
                        }
                        catch (DmException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parms">SQL参数</param>
        /// <returns>查询结果</returns>
        public object GetSingle(string sql, params DmParameter[] parms)
        {
            using (DmConnection conn = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand())
                {
                    PrepareCommand(cmd, conn, null, sql, parms);

                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();

                    if ((object.Equals(obj, null)) || (object.Equals(obj, DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parms">SQL参数</param>
        /// <returns>查询结果</returns>
        public async Task<object> GetSingleAsync(string sql, params DmParameter[] parms)
        {
            using (DmConnection conn = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand())
                {
                    await PrepareCommandAsync(cmd, conn, null, sql, parms);

                    object obj = await cmd.ExecuteScalarAsync();
                    cmd.Parameters.Clear();

                    if ((object.Equals(obj, null)) || (object.Equals(obj, DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DmDataReader (切记要手工关闭DmDataReader)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parms">SQL参数</param>
        /// <returns>DmDataReader</returns>
        public DbDataReader ExecuteReader(string sql, params DmParameter[] parms)
        {
            DmConnection connection = new DmConnection(connectionString);
            DmCommand cmd = new DmCommand();

            PrepareCommand(cmd, connection, null, sql, parms);

            DbDataReader myReader = cmd.ExecuteReader();
            cmd.Parameters.Clear();

            return myReader;
        }

        /// <summary>
        /// 执行查询语句，返回DmDataReader (切记要手工关闭DmDataReader)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parms">SQL参数</param>
        /// <returns>DmDataReader</returns>
        public async Task<DbDataReader> ExecuteReaderAsync(string sql, params DmParameter[] parms)
        {
            DmConnection connection = new DmConnection(connectionString);
            DmCommand cmd = new DmCommand();

            await PrepareCommandAsync(cmd, connection, null, sql, parms);

            var myReader = await cmd.ExecuteReaderAsync();
            cmd.Parameters.Clear();
            return myReader;
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="paras">参数数组</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sql, params DmParameter[] paras)
        {
            using (DmConnection connection = new DmConnection(connectionString))
            {
                using (DmCommand cmd = new DmCommand())
                {
                    PrepareCommand(cmd, connection, null, sql, paras);
                    DataSet ds = new DataSet();

                    using (DmDataAdapter da = new DmDataAdapter(cmd))
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();

                        return ds;
                    }
                }
            }
        }

        /// <summary>
        /// 准备SQL查询命令
        /// </summary>
        /// <param name="cmd">SQL命令对象</param>
        /// <param name="conn">SQL连接对象</param>
        /// <param name="trans">SQL事务对象</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="paras">SQL参数数组</param>
        private void PrepareCommand(DmCommand cmd, DmConnection conn, DbTransaction trans, string cmdText, DmParameter[] paras)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            cmd.CommandType = CommandType.Text;
            if (paras != null)
            {
                foreach (DmParameter parameter in paras)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 准备SQL查询命令
        /// </summary>
        /// <param name="cmd">SQL命令对象</param>
        /// <param name="conn">SQL连接对象</param>
        /// <param name="trans">SQL事务对象</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="paras">SQL参数数组</param>
        private async Task PrepareCommandAsync(DmCommand cmd, DmConnection conn, DbTransaction trans, string cmdText, DmParameter[] paras)
        {
            if (conn.State != ConnectionState.Open)
            {
                await conn.OpenAsync();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            cmd.CommandType = CommandType.Text;
            if (paras != null)
            {
                foreach (DmParameter parameter in paras)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion 执行带参数的SQL语句
    }
使用方法也很简单，传入SQL语句和参数即可。这里给出几个增删改查的例子：

    public class PersonAdoNetDAL : IPersonDAL
    {
        static readonly DmDbClient _client = new DmDbClient("Server=127.0.0.1; UserId=TESTDB; PWD=1234567");

        public int Add(PersonModel model)
        {
            string sql = "insert into Person(Name,City) Values(:Name,:City)";
            DmParameter[] paras = new DmParameter[] {
                new DmParameter(":Name",model.Name),
                new DmParameter(":City",model.City)
            };

            return _client.ExecuteAdd(sql, paras);
        }

        public bool Update(PersonModel model)
        {
            string sql = "update Person set City=:City where Id=:Id";
            DmParameter[] paras = new DmParameter[] {
                new DmParameter(":Id",model.Id),
                new DmParameter(":City",model.City)
            };

            return _client.ExecuteSql(sql, paras) > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            string sql = "delete from Person where Id=:Id";
            DmParameter[] paras = new DmParameter[] {
                new DmParameter(":Id",id),
            };

            return _client.ExecuteSql(sql, paras) > 0 ? true : false;
        }

        public PersonModel Get(int id)
        {
            string sql = "select Id,Name,City from Person where Id=:Id";
            DmParameter[] paras = new DmParameter[] {
                new DmParameter(":Id",id),
            };

            PersonModel model = null;
            using (var reader = (DmDataReader)_client.ExecuteReader(sql, paras))
            {
                while (reader.Read())
                {
                    model = new PersonModel();
                    model.Id = reader.GetInt32(0);
                    model.Name = reader.GetString(1);
                    model.City = reader.GetString(2);
                }
            }

            return model;
        }

        public List<PersonModel> GetList()
        {
            var list = new List<PersonModel>();
            using (var reader = (DmDataReader)_client.ExecuteReader("select Id,Name,City from Person"))
            {
                while (reader.Read())
                {
                    var model = new PersonModel();
                    model.Id = reader.GetInt32(0);
                    model.Name = reader.GetString(1);
                    model.City = reader.GetString(2);
                    list.Add(model);
                }
            }

            return list;
        }

    }