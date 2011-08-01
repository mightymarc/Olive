CREATE FUNCTION [dbo].[GetSetting]
(
	@SettingId VARCHAR(50)
)
RETURNS VARCHAR(200)
AS

BEGIN
	DECLARE @Result VARCHAR(200);
	SELECT @Result = [Value] FROM dbo.[Setting] WHERE SettingId = @SettingId;

	RETURN @Result;
END