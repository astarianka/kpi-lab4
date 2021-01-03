using NUnit.Framework;
using System;
using IIG.BinaryFlag;

namespace lab4_1
{
    [TestFixture()]
    public class FlagTests
    {
        UInt64 max_bits = 17179868704;
        UInt64 min_bits = 2;
        /* Test MultipleBinaryFlag constructor for invalid input */
        [Test()]
        public void TestConstructorInvalidInputDetected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(min_bits - 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(max_bits + 1));
        }

        /* Test MultipleBinaryFlag constructor for extreme input */
        [Test()]
        public void TestConstructorExtremeInput()
        {
            Assert.NotNull(new MultipleBinaryFlag(min_bits));
            Assert.NotNull(new MultipleBinaryFlag(max_bits));
        }

        /* Common varibles for tests */
        static ushort test_length = 10;
        MultipleBinaryFlag flag_1_default = new MultipleBinaryFlag(test_length);
        MultipleBinaryFlag flag_2_default = new MultipleBinaryFlag(test_length);

        /* Test GetType returns MultipleBinaryFlag type*/
        [Test()]
        public void TestGetType()
        {
            Assert.AreEqual(flag_1_default.GetType().ToString(), "IIG.BinaryFlag.MultipleBinaryFlag");
        }

        /* Test that object references are different despite identical arguments*/
        [Test()]
        public void TestReferencesNotEqual()
        {
            Assert.AreNotEqual(flag_1_default, flag_2_default);
        }

        /* Test that GetFlag method return is valid*/
        [Test()]
        public void TestGetFlagIsValid()
        {
            Assert.NotNull(flag_1_default.GetFlag());
        }

        /* Test that objects with same initial configurations return the same flag status*/
        [Test()]
        public void TestFlagsEqual()
        {
            Assert.AreEqual(flag_1_default.GetFlag(), flag_2_default.GetFlag());
        }

        /* Test if it is possible to access bit out of flag range */
        [Test()]
        public void TestSetFlagInvalidInputDetected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => flag_1_default.SetFlag((ulong)test_length + 1));
        }

        /* Test that SetFlag does change bit value to 1 */
        [Test()]
        public void TestSetFlagChangeValue()
        {
            MultipleBinaryFlag negative_flag = new MultipleBinaryFlag(test_length, false);
            Assert.AreEqual(negative_flag.GetFlag(), false);

            for (ushort i = 0; i < test_length; i++)
            {
                negative_flag.SetFlag(i);
            }

            Assert.AreEqual(negative_flag.GetFlag(), true);
        }

        /* Test that ResetFlag does change bit value to 0 */
        [Test()]
        public void TestResetFlagChangeValue()
        {
            MultipleBinaryFlag positive_flag = new MultipleBinaryFlag(test_length, true);
            Assert.AreEqual(positive_flag.GetFlag(), true);

            for (ushort i = 0; i < test_length; i++)
            {
                positive_flag.ResetFlag(i);
            }

            Assert.AreEqual(positive_flag.GetFlag(), false);
        }

        /* Test that if at least one bit is 0 then the whole flag is False*/
        [Test()]
        public void TestResetFlagOneBitMatters()
        {
            MultipleBinaryFlag positive_flag = new MultipleBinaryFlag(test_length, true);
            Assert.AreEqual(positive_flag.GetFlag(), true);

            positive_flag.ResetFlag(3);

            Assert.AreEqual(positive_flag.GetFlag(), false);
        }

        /* Test correctness of ToString method */
        [Test()]
        public void TestToStringCorrect()
        {
            MultipleBinaryFlag flag_1 = new MultipleBinaryFlag(test_length, true);
            MultipleBinaryFlag flag_2 = new MultipleBinaryFlag(test_length, false);

            Assert.AreEqual(flag_1.ToString(), "TTTTTTTTTT");
            Assert.AreEqual(flag_2.ToString(), "FFFFFFFFFF");

            flag_1.ResetFlag(5);
            flag_2.SetFlag(5);

            Assert.AreEqual(flag_1.ToString(), "TTTTTFTTTT");
            Assert.AreEqual(flag_2.ToString(), "FFFFFTFFFF");

            flag_1.ResetFlag(0);
            flag_2.SetFlag(0);
            flag_1.ResetFlag((ulong)test_length - 1);
            flag_2.SetFlag((ulong)test_length - 1);

            Assert.AreEqual(flag_1.ToString(), "FTTTTFTTTF");
            Assert.AreEqual(flag_2.ToString(), "TFFFFTFFFT");
        }

        /* Test that Dispose method does clear object resources*/
        [Test()]
        public void TestDispose()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(test_length, false);

            flag.SetFlag(3);
            Assert.AreEqual(flag.ToString(), "FFFTFFFFFF");

            flag.Dispose();

            Assert.Null(flag);
        }
    }
}
