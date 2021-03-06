﻿using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class BasicAccountTests
    {
        [TestCase("33333", "Basic Account", 100, AccountType.Free, 250, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -100, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 250, 350, true)]
        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount,decimal expectedBalance, bool expectedResult)
        {
            IDeposit depositResponse = new NoLimitDepositRule();

            Account accountVariable = new Account()
            {

                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = accountType
            };
            AccountDepositResponse accountDepositResponse = depositResponse.Deposit(accountVariable, amount);
            Assert.AreEqual(expectedResult, accountDepositResponse.Success);                      
            Assert.AreEqual(expectedBalance, accountDepositResponse.Account.Balance);
            
        }
        [TestCase("33333", "Basic Account", 1500, AccountType.Basic, -1000,1500, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Free, -100, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 100, 100, false)]
        [TestCase("33333", "Basic Account", 150, AccountType.Basic, -50, 100, true)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -150, -60, true)]
        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount,decimal expectedBalance, bool expectedResult)
        {
            IWithdraw withdrawResponse = new BasicAccountWithdrawRule();

            Account accountVariable = new Account()
            {

                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = accountType
                
            };
            AccountWithdrawResponse accountWithdrawResponse = withdrawResponse.Withdraw(accountVariable, amount);
            Assert.AreEqual(expectedResult, accountWithdrawResponse.Success);
            Assert.AreEqual(expectedBalance, accountWithdrawResponse.Account.Balance);
            
        }

    }
}
