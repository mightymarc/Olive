-- =============================================
-- Author: Phil Hannent
-- Create date: 29/10/2007
-- Description: Round up
-- =============================================
CREATE FUNCTION dbo.RoundUp
(
	@Num1 FLOAT
)
RETURNS FLOAT
AS
BEGIN
DECLARE @Temp FLOAT
SET @Temp = 0

IF @Num1 - CAST(ROUND(@Num1, 0) AS FLOAT) > 0
BEGIN
SET @Temp = 1
END

RETURN ROUND(@Num1, 0) + @Temp

END
GO