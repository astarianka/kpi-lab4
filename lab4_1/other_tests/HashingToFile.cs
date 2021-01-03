using NUnit.Framework;
using System;
using IIG.FileWorker;
using IIG.PasswordHashingUtils;

namespace lab4_1.other_tests
{
    [TestFixture()]
    public class HashingToFile
    {
        private string test_file = "/home/anna/Documents/4TH_COURSE/TESTING/SOURCES/lab4/lab4_1/lab4_1/lab4_1/files_for_test/test_file.txt";
        private string test_expr = "test_string";
        private string salt_expr = "test_salt";

        [Test()]
        public void TestWriteHash()
        {
            Assert.IsTrue(BaseFileWorker.Write(PasswordHasher.GetHash(test_expr, salt_expr), test_file));
        }

        [Test()]
        public void TestReadHash()
        {
            string[] hashes_from_file = BaseFileWorker.ReadLines(test_file);
            Assert.AreEqual(PasswordHasher.GetHash(test_expr, salt_expr), hashes_from_file[0]);
        }
    }
}
