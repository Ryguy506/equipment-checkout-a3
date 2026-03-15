using EquipmentCheckout.Domain.Services;
using EquipmentCheckout.Domain.Dtos.Requests;
using EquipmentCheckout.Domain.Entities;
using EquipmentCheckout.Domain.Daos;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentCheckout.Domain.Dtos;

public class CheckoutServiceTests
{
    private CheckoutService CreateService(
        TestEquipmentDao equipmentDao,
        TestBorrowerDao borrowerDao,
        TestLoanDao loanDao,
        TestHoldDao holdDao)
    {
        return new CheckoutService(equipmentDao, borrowerDao, loanDao, holdDao);
    }

    [Fact]
    public void CannotLoanItemAlreadyOnLoan()
    {
        var equipmentDao = new TestEquipmentDao();
        var borrowerDao = new TestBorrowerDao();
        var loanDao = new TestLoanDao();
        var holdDao = new TestHoldDao();

        equipmentDao.ItemOnLoan = true;

        var service = CreateService(equipmentDao, borrowerDao, loanDao, holdDao);

        var result = service.CreateLoan(new LoanRequest(1,1,5));

        Assert.False(result.Success);
    }

    [Fact]
    public void BorrowerCannotHaveMoreThanTwoLoans()
    {
        var equipmentDao = new TestEquipmentDao();
        var borrowerDao = new TestBorrowerDao();
        var loanDao = new TestLoanDao();
        var holdDao = new TestHoldDao();

        borrowerDao.ActiveLoanCount = 2;

        var service = CreateService(equipmentDao, borrowerDao, loanDao, holdDao);

        var result = service.CreateLoan(new LoanRequest(1,1,5));

        Assert.False(result.Success);
    }

    [Fact]
    public void InvalidLoanDurationRejected()
    {
        var equipmentDao = new TestEquipmentDao();
        var borrowerDao = new TestBorrowerDao();
        var loanDao = new TestLoanDao();
        var holdDao = new TestHoldDao();

        var service = CreateService(equipmentDao, borrowerDao, loanDao, holdDao);

        var result = service.CreateLoan(new LoanRequest(1,1,20));

        Assert.False(result.Success);
    }

    [Fact]
    public void CannotLoanInactiveItem()
    {
        var equipmentDao = new TestEquipmentDao();
        equipmentDao.ItemIsActive = false;

        var borrowerDao = new TestBorrowerDao();
        var loanDao = new TestLoanDao();
        var holdDao = new TestHoldDao();

        var service = CreateService(equipmentDao, borrowerDao, loanDao, holdDao);

        var result = service.CreateLoan(new LoanRequest(1,1,5));

        Assert.False(result.Success);
    }

    [Fact]
    public void CannotLoanToInactiveBorrower()
    {
        var equipmentDao = new TestEquipmentDao();
        var borrowerDao = new TestBorrowerDao();
        borrowerDao.IsActive = false;

        var loanDao = new TestLoanDao();
        var holdDao = new TestHoldDao();

        var service = CreateService(equipmentDao, borrowerDao, loanDao, holdDao);

        var result = service.CreateLoan(new LoanRequest(1,1,5));

        Assert.False(result.Success);
    }

    [Fact]
    public void HoldQueueOrderRespected()
    {
        var equipmentDao = new TestEquipmentDao();
        var borrowerDao = new TestBorrowerDao();
        var loanDao = new TestLoanDao();
        var holdDao = new TestHoldDao();

        equipmentDao.ItemOnLoan = true;

        var service = CreateService(equipmentDao, borrowerDao, loanDao, holdDao);

        service.PlaceHold(new HoldRequest(1, 1));
        service.PlaceHold(new HoldRequest(2, 1));

        var firstHold = holdDao.GetFirstHoldForItem(1);

        Assert.NotNull(firstHold);
        Assert.Equal(1, firstHold.BorrowerId);
    }
}