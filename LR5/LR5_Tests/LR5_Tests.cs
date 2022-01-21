using NUnit.Framework;

namespace LR5.Tests
{
    public class Tests
    {
        [Test]
        public void AddMoney_120RUB_120return()
        {
            Wallet wallet = new Wallet();

            wallet.AddMoney("RUB", 120);
            int index = wallet.currencyList.FindIndex(x => x == "RUB");
            double expected = 120;

            double actual = wallet.countList[index];
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddMoney_120RUB_x3_360return()
        {
            Wallet wallet = new Wallet();

            wallet.AddMoney("RUB", 120);
            wallet.AddMoney("RUB", 120);
            wallet.AddMoney("RUB", 120);
            int index = wallet.currencyList.FindIndex(x => x == "RUB");
            double expected = 360;

            double actual = wallet.countList[index];
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddMoney_800TNG_800return()
        {
            Wallet wallet = new Wallet();

            wallet.AddMoney("TNG", 800);
            int index = wallet.currencyList.FindIndex(x => x == "TNG");
            double expected = 800;

            double actual = wallet.countList[index];
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddMoney_1200RUB_RemoveMoney_150EUR_1050return()
        {
            Wallet wallet = new Wallet();

            wallet.AddMoney("RUB", 1200);
            wallet.RemoveMoney("RUB", 150);
            int index = wallet.currencyList.FindIndex(x => x == "RUB");
            double expected = 1050;

            double actual = wallet.countList[index];
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Convert_100GBP_RUB_9000return()
        {
            Wallet wallet = new Wallet();
            Bank bank = new Bank();
            wallet.CreateCurrency();
            //wallet.AddMoney("RUB", 100);
            //wallet.AddMoney("USD", 100);
            //wallet.AddMoney("EUR", 100);
            wallet.AddMoney("GBP", 100);
            double expected = 9000;

            double actual = bank.Convert(wallet.countList[3], wallet.cursList[3], wallet.cursList[0]);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTotalMoney_100RUB_100USD_100EUR_100GBP_RUB_24100return() // Не будет выполняться при нормальных условиях, так как метод GetTotalMoney вызывает метод рандомизации курсов валют, и каждый раз ответ будет разным
        {
            Wallet wallet = new Wallet();
            wallet.CreateCurrency();
            wallet.AddMoney("RUB", 100);
            wallet.AddMoney("USD", 100);
            wallet.AddMoney("EUR", 100);
            wallet.AddMoney("GBP", 100);
            double expected = 24100;

            double actual = wallet.GetTotalMoney("RUB");
            Assert.AreEqual(expected, actual);
        }
    }
}