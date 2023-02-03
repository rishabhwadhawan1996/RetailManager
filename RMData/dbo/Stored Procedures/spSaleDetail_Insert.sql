CREATE PROCEDURE [dbo].[spSaleDetail_Insert]
	@SaleId int output,
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@Tax money
AS
begin
	set nocount on;
	insert into dbo.SaleDetail(SaleId,ProductId,Quantity,Tax,PurchasePrice)
	values (@SaleId,@ProductId,@Quantity,@Tax,@PurchasePrice);
end
