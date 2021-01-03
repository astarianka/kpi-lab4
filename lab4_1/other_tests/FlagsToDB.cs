using NUnit.Framework;
using System;
using IIG.DatabaseConnectionUtils;
using IIG.CoSFE.DatabaseUtils;
using IIG.BinaryFlag;

namespace lab4_1.other_tests
{
    [TestFixture()]
    public class FlagsToDB
    {
        private const string Server = @"localhost";
        private const string Database = @"IIG.CoSWE.FlagpoleDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"12345_Anna";
        private const int ConnectionTime = 75;

        FlagpoleDatabaseUtils DBUtil = new FlagpoleDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);
        static ushort test_length = 10;
        MultipleBinaryFlag flag_negative = new MultipleBinaryFlag(test_length, false);
        MultipleBinaryFlag flag_positive = new MultipleBinaryFlag(test_length, true);

        [Test()]
        public void TestAddFlag()
        {
            Assert.IsTrue(DBUtil.AddFlag("f", flag_negative.GetFlag()));
            Assert.IsTrue(DBUtil.AddFlag("t", flag_positive.GetFlag()));
            Assert.IsTrue(DBUtil.AddFlag("FF", flag_negative.GetFlag()));
            Assert.IsTrue(DBUtil.AddFlag("TT", flag_positive.GetFlag()));
        }

        [Test()]
        public void TestAddFlagWrong()
        {
            Assert.IsFalse(DBUtil.AddFlag("random_1", flag_negative.GetFlag()));
            Assert.IsFalse(DBUtil.AddFlag("random_2", flag_positive.GetFlag()));
        }

        [Test()]
        public void TestGetFromDBInvalid()
        {
            string flag_view;
            bool? flag_val;

            Assert.IsFalse(DBUtil.GetFlag(-1, out flag_view, out flag_val));
            Assert.IsFalse(DBUtil.GetFlag(100, out flag_view, out flag_val));
        }

        [Test()]
        public void TestGetFromDB()
        {
            string flag_view;
            bool? flag_val;

            DBUtil.GetFlag(2, out flag_view, out flag_val);
            Assert.AreEqual("t", flag_view);
            Assert.AreEqual(true, flag_val);
        }
    }
}
