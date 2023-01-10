using Dapper;
using Npgsql;

namespace Discount.Grpc.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly string _connectionString;

    public DiscountRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<Coupon?> GetDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
            "SELECT * FROM Coupon WHERE ProductName=@productName", new { productName });

        return coupon;
    }

    public async Task<Coupon> CreateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        coupon.Id = await connection.ExecuteScalarAsync<int>(
            "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount) RETURNING Id", coupon);
        return coupon;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var affected = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id", coupon);
        return affected > 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var affected = await connection.ExecuteAsync(
            "DELETE FROM Coupon WHERE ProductName=@productName", new { productName });
        return affected > 0;
    }
}