CREATE PROCEDURE dbo.RethrowError AS
        IF ERROR_NUMBER() IS NULL
                RETURN;

        DECLARE 
                @ErrorMessage    NVARCHAR(4000),
                @ErrorNumber     INT,
                @ErrorSeverity   INT,
                @ErrorState      INT,
                @ErrorLine       INT,
                @ErrorProcedure  NVARCHAR(200);

        SELECT 
                @ErrorNumber = ERROR_NUMBER(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE(),
                @ErrorLine = ERROR_LINE(),
                @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

        SELECT @ErrorMessage = 
                N'Error %d, Level %d, State %d, Procedure %s, Line %d, ' + 
                        'Message: '+ ERROR_MESSAGE();

        RAISERROR 
                (
                @ErrorMessage, 
                @ErrorSeverity, 
                @ErrorState,               
                @ErrorNumber,    -- parameter: original error number.
                @ErrorSeverity,  -- parameter: original error severity.
                @ErrorState,     -- parameter: original error state.
                @ErrorProcedure, -- parameter: original error procedure name.
                @ErrorLine       -- parameter: original error line number.
                );