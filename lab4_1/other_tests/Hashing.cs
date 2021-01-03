using NUnit.Framework;
using System;
using IIG.PasswordHashingUtils;

namespace lab4_1.other_tests
{
    [TestFixture()]
    public class Hashing
    {
        /* Test general workability */
        [Test()]
        public void TestResultPresent()
        {
            string test_expr = "test_string";

            string result = PasswordHasher.GetHash(test_expr);

            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test()]
        public void TestResultHashed()
        {
            string test_expr = "test_string";

            Assert.AreNotEqual(test_expr, PasswordHasher.GetHash(test_expr));
        }

        /* Test input arguments tolerance */
        [Test()]
        public void TestInputEmpty()
        {
            string test_expr = "";

            string result = PasswordHasher.GetHash(test_expr);

            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test()]
        public void TestInputCyrillic()
        {
            string test_expr = "тестове_значення";

            string result = PasswordHasher.GetHash(test_expr);

            Assert.AreNotEqual(test_expr, result);
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test()]
        public void TestInputSpecials()
        {
            string test_expr = "X Æ A-12";

            string result = PasswordHasher.GetHash(test_expr);

            Assert.AreNotEqual(test_expr, result);
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }

        /* Test Init method workability */
        [Test()]
        public void TestInitMethod()
        {
            string test_expr = "test_string";

            string result_before_init = PasswordHasher.GetHash(test_expr);

            PasswordHasher.Init("custom_salt", 1234);

            string result_after_init = PasswordHasher.GetHash(test_expr);

            Assert.AreNotEqual(test_expr, result_before_init);
            Assert.AreNotEqual(test_expr, result_after_init);
            Assert.AreNotEqual(result_before_init, result_after_init);
        }

        /* Test that default arguments are set to null values*/
        [Test()]
        public void TestDefaultsAreNull()
        {
            string test_expr = "test_string";

            string result_benchmark = PasswordHasher.GetHash(test_expr);
            string result_hashed_with_null = PasswordHasher.GetHash(test_expr, "", null);

            PasswordHasher.Init("", 0);

            string result_after_init = PasswordHasher.GetHash(test_expr);

            Assert.AreNotEqual(test_expr, result_benchmark);
            Assert.AreNotEqual(test_expr, result_hashed_with_null);
            Assert.AreNotEqual(test_expr, result_after_init);
            Assert.AreEqual(result_benchmark, result_hashed_with_null);
            Assert.AreEqual(result_benchmark, result_after_init);
        }

        /* Test hash result are equal if arguments are equal */

        [Test()]
        public void TestSameArgs()
        {
            string test_expr = "test_string";
            string salt_expr = "salt_string";
            uint adler_val = 1234;
            string test_expr_dublicate = test_expr;
            string result_benchmark;
            string result_dublicate;

            result_benchmark = PasswordHasher.GetHash(test_expr);
            result_dublicate = PasswordHasher.GetHash(test_expr_dublicate);

            Assert.AreEqual(result_benchmark, result_dublicate);

            result_benchmark = PasswordHasher.GetHash(test_expr, salt_expr);
            result_dublicate = PasswordHasher.GetHash(test_expr_dublicate, salt_expr);

            Assert.AreEqual(result_benchmark, result_dublicate);

            result_benchmark = PasswordHasher.GetHash(test_expr, salt_expr, adler_val);
            result_dublicate = PasswordHasher.GetHash(test_expr_dublicate, salt_expr, adler_val);

            Assert.AreEqual(result_benchmark, result_dublicate);
        }

        /* Test hash result are different if arguments are not equal */
        [Test()]
        public void TestDifferentPass()
        {
            string test_expr = "test_string";
            string test_expr_different = "test_string_different";
            string salt_expr = "salt_string";
            uint adler_val = 1234;
            string result_benchmark;
            string result_different;

            result_benchmark = PasswordHasher.GetHash(test_expr);
            result_different = PasswordHasher.GetHash(test_expr_different);

            Assert.AreNotEqual(result_benchmark, result_different);

            result_benchmark = PasswordHasher.GetHash(test_expr, salt_expr);
            result_different = PasswordHasher.GetHash(test_expr_different, salt_expr);

            Assert.AreNotEqual(result_benchmark, result_different);

            result_benchmark = PasswordHasher.GetHash(test_expr, salt_expr, adler_val);
            result_different = PasswordHasher.GetHash(test_expr_different, salt_expr, adler_val);

            Assert.AreNotEqual(result_benchmark, result_different);
        }

        [Test()]
        public void TestDifferentConfigs()
        {
            string test_expr = "test_string";
            string salt_expr = "salt_string";
            uint adler_val = 1234;
            string salt_expr_different = "salt_string_different";
            uint adler_val_different = 4321;
            string test_expr_dublicate = test_expr;
            string result_benchmark;
            string result_different;

            result_benchmark = PasswordHasher.GetHash(test_expr);
            result_different = PasswordHasher.GetHash(test_expr_dublicate);

            Assert.AreEqual(result_benchmark, result_different);

            result_benchmark = PasswordHasher.GetHash(test_expr, salt_expr);
            result_different = PasswordHasher.GetHash(test_expr_dublicate, salt_expr_different);

            Assert.AreNotEqual(result_benchmark, result_different);

            result_benchmark = PasswordHasher.GetHash(test_expr, salt_expr, adler_val);
            result_different = PasswordHasher.GetHash(test_expr_dublicate, salt_expr_different, adler_val_different);

            Assert.AreNotEqual(result_benchmark, result_different);

        }

    }
}
