using Moq;

namespace lab31v13.Tests;

public class SubscriptionServiceTests
{
    [Fact]
    public void GetSubscription_ReturnsSubscription_WhenExists()
    {
        var mockRepo = new Mock<ISubscriptionRepository>();
        var mockBilling = new Mock<IBillingService>();
        var expected = new Subscription { Id = 1, UserId = 10, Plan = "Basic", IsActive = true };
        mockRepo.Setup(r => r.GetById(1)).Returns(expected);
        var service = new SubscriptionService(mockRepo.Object, mockBilling.Object);

        var result = service.GetSubscription(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public void GetSubscription_ThrowsKeyNotFoundException_WhenNotFound()
    {
        var mockRepo = new Mock<ISubscriptionRepository>();
        var mockBilling = new Mock<IBillingService>();
        mockRepo.Setup(r => r.GetById(99)).Returns((Subscription?)null);
        var service = new SubscriptionService(mockRepo.Object, mockBilling.Object);

        Action action = () => service.GetSubscription(99);

        Assert.Throws<KeyNotFoundException>(action);
    }

    [Fact]
    public void Subscribe_ReturnsTrue_WhenChargeSucceeds()
    {
        var mockRepo = new Mock<ISubscriptionRepository>();
        var mockBilling = new Mock<IBillingService>();
        mockBilling.Setup(b => b.Charge(7, 25m)).Returns(true);
        var service = new SubscriptionService(mockRepo.Object, mockBilling.Object);

        var result = service.Subscribe(7, "Pro", 25m);

        Assert.True(result);
        mockRepo.Verify(r => r.Save(It.IsAny<Subscription>()), Times.Once);
        mockBilling.Verify(b => b.Charge(7, 25m), Times.Once);
    }

    [Fact]
    public void Subscribe_ReturnsFalse_WhenChargeFails()
    {
        var mockRepo = new Mock<ISubscriptionRepository>();
        var mockBilling = new Mock<IBillingService>();
        mockBilling.Setup(b => b.Charge(7, 25m)).Returns(false);
        var service = new SubscriptionService(mockRepo.Object, mockBilling.Object);

        var result = service.Subscribe(7, "Pro", 25m);

        Assert.False(result);
        mockRepo.Verify(r => r.Save(It.IsAny<Subscription>()), Times.Never);
        mockBilling.Verify(b => b.Charge(7, 25m), Times.Once);
    }

    [Fact]
    public void CancelSubscription_SetsIsActiveFalse_AndSaves()
    {
        var mockRepo = new Mock<ISubscriptionRepository>();
        var mockBilling = new Mock<IBillingService>();
        var subscription = new Subscription
        {
            Id = 5,
            UserId = 11,
            Plan = "Enterprise",
            StartDate = DateTime.UtcNow.AddDays(-10),
            EndDate = DateTime.UtcNow.AddDays(20),
            IsActive = true
        };
        mockRepo.Setup(r => r.GetById(5)).Returns(subscription);
        var service = new SubscriptionService(mockRepo.Object, mockBilling.Object);

        var result = service.CancelSubscription(5);

        Assert.True(result);
        mockRepo.Verify(r => r.Save(It.Is<Subscription>(s => s.IsActive == false && s.Id == 5)), Times.Once);
        mockBilling.Verify(b => b.Refund(11, It.IsAny<decimal>()), Times.Once);
    }

    [Fact]
    public void CancelSubscription_ThrowsKeyNotFoundException_WhenNotFound()
    {
        var mockRepo = new Mock<ISubscriptionRepository>();
        var mockBilling = new Mock<IBillingService>();
        mockRepo.Setup(r => r.GetById(404)).Returns((Subscription?)null);
        var service = new SubscriptionService(mockRepo.Object, mockBilling.Object);

        Action action = () => service.CancelSubscription(404);

        Assert.Throws<KeyNotFoundException>(action);
    }

    [Fact]
    public void RenewSubscription_ExtendsEndDate_WhenChargeSucceeds()
    {
        var mockRepo = new Mock<ISubscriptionRepository>();
        var mockBilling = new Mock<IBillingService>();
        var originalEndDate = DateTime.UtcNow.AddDays(5);
        var subscription = new Subscription
        {
            Id = 3,
            UserId = 9,
            Plan = "Pro",
            StartDate = DateTime.UtcNow.AddDays(-25),
            EndDate = originalEndDate,
            IsActive = true
        };
        mockRepo.Setup(r => r.GetById(3)).Returns(subscription);
        mockBilling.Setup(b => b.Charge(9, 15m)).Returns(true);
        var service = new SubscriptionService(mockRepo.Object, mockBilling.Object);

        var result = service.RenewSubscription(3, 15m);

        Assert.True(result);
        mockRepo.Verify(r => r.Save(It.Is<Subscription>(s => s.EndDate > originalEndDate)), Times.Once);
        Assert.Equal(originalEndDate.AddDays(30), subscription.EndDate);
    }

    [Fact]
    public void IsActive_ReturnsFalse_WhenSubscriptionNotFound()
    {
        var mockRepo = new Mock<ISubscriptionRepository>();
        var mockBilling = new Mock<IBillingService>();
        mockRepo.Setup(r => r.GetById(123)).Returns((Subscription?)null);
        var service = new SubscriptionService(mockRepo.Object, mockBilling.Object);

        var result = service.IsActive(123);

        Assert.False(result);
    }
}