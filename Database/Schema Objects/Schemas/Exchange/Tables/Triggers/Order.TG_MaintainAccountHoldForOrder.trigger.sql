CREATE TRIGGER [TG_MaintainAccountHoldForOrder]
    ON [Exchange].[Order]
    FOR DELETE, INSERT, UPDATE 
    AS 
    BEGIN
    	SET NOCOUNT ON


    END
