namespace CS_semester_3.Tests; 

public class PartitionTests {
    [Fact]
    public void Should_Set_Memory_To_Zero_When_Changing_System_Type() {
        // Arrange
        var partition = new Partition("", 0, 1000, SystemType.FAT, 250);
        ulong expectedMemoryUsed = 0;
        
        // Act
        partition.SystemType = SystemType.ext4;
        
        // Assert
        Assert.Equal(partition.MemoryUsed, expectedMemoryUsed);
    }
}