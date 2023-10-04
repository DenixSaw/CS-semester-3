namespace CS_semester_3.Tests;

public class DiskTests {
    [Fact]
    public void Should_Change_Length_When_Deleting_Partition() {
        // Arrange
        var disk = new Disk("Samsung", "870", "123", Type.SSD, 1000 * 1024 * 1024, TableType.GPT);
        disk.AddPartition(new Partition("", 0, 1000, SystemType.FAT, 250));
        var expectedLength = 0;
        
        // Act
        disk.RemovePartition(0);
        
        // Assert
        Assert.Equal(disk.Partitions.Count, expectedLength);
    }
    
    [Fact]
    public void Should_Throw_Error_When_Delete_Index_Out_Of_Bounds() {
        // Arrange
        var disk = new Disk("Samsung", "870", "123", Type.SSD, 1000 * 1024 * 1024, TableType.GPT);
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => disk.RemovePartition(4));
    }
    
    [Fact]
    public void Should_Throw_Error_When_Partitions_Intersect() {
        // Arrange
        var disk = new Disk("Samsung", "870", "123", Type.SSD, 1000 * 1024 * 1024, TableType.GPT);
        disk.AddPartition(new Partition("", 0, 1000, SystemType.FAT, 250));
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            disk.AddPartition(new Partition("", 50, 100, SystemType.FAT, 250)));
    }
    
    [Fact]
    public void Should_Throw_Error_When_Partitions_Exceed_Memory() {
        // Arrange
        var disk = new Disk("Samsung", "870", "123", Type.SSD, 1024, TableType.GPT);
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            disk.AddPartition(new Partition("", 0, 2048, SystemType.FAT, 250)));
    }
}