﻿namespace CSharpTests
{
    public class EmployeeIdTests
    {
        [Fact]
        void ZeroValueCanCreate()
        {
            var result = new EmpoloyeeId(0);
            result.Should().NotBeNull();
        }
    }
}
