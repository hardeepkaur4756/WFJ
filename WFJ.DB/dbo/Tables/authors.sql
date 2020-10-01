CREATE TABLE [dbo].[authors] (
    [authorID]                         INT           IDENTITY (1, 1) NOT NULL,
    [loginName]                        VARCHAR (20)  NULL,
    [loginPassword]                    VARCHAR (50)  NULL,
    [authorFName]                      VARCHAR (50)  NULL,
    [authorLName]                      VARCHAR (50)  NULL,
    [authorEmail]                      VARCHAR (50)  NULL,
    [authorPhone]                      VARCHAR (50)  NULL,
    [authorLevel]                      INT           NULL,
    [authorEmailNotification]          TINYINT       NULL,
    [toolsControl]                     VARCHAR (512) NULL,
    [tableControl]                     VARCHAR (512) NULL,
    [sectionAccess]                    VARCHAR (512) NULL,
    [pageAccess]                       VARCHAR (512) NULL,
    [EWImageDir]                       VARCHAR (50)  NULL,
    [EWShowFullScreenButton]           TINYINT       NULL,
    [EWShowBoldButton]                 TINYINT       NULL,
    [EWShowUnderlineButton]            TINYINT       NULL,
    [EWShowItalicButton]               TINYINT       NULL,
    [EWShowStrikethroughButton]        TINYINT       NULL,
    [EWShowNumberListButton]           TINYINT       NULL,
    [EWShowBulletListButton]           TINYINT       NULL,
    [EWShowDecreaseIndentButton]       TINYINT       NULL,
    [EWShowIncreaseIndentButton]       TINYINT       NULL,
    [EWShowLeftAlignButton]            TINYINT       NULL,
    [EWShowCenterAlignButton]          TINYINT       NULL,
    [EWShowRightAlignButton]           TINYINT       NULL,
    [EWShowJustifyButton]              TINYINT       NULL,
    [EWShowHorizontalRuleButton]       TINYINT       NULL,
    [EWShowLinkButton]                 TINYINT       NULL,
    [EWShowAnchorButton]               TINYINT       NULL,
    [EWShowMailLinkButton]             TINYINT       NULL,
    [EWShowHelpButton]                 TINYINT       NULL,
    [EWShowFontList]                   TINYINT       NULL,
    [EWShowSizeList]                   TINYINT       NULL,
    [EWShowFormatList]                 TINYINT       NULL,
    [EWShowStyleList]                  TINYINT       NULL,
    [EWShowForeColorButton]            TINYINT       NULL,
    [EWShowBackColorButton]            TINYINT       NULL,
    [EWShowTableButton]                TINYINT       NULL,
    [EWShowFormButton]                 TINYINT       NULL,
    [EWShowImageButton]                TINYINT       NULL,
    [EWShowTextBoxButton]              TINYINT       NULL,
    [EWShowSymbolButton]               TINYINT       NULL,
    [EWShowPropertiesButton]           TINYINT       NULL,
    [EWShowCleanHTMLButton]            TINYINT       NULL,
    [EWShowAbsolutePositionButton]     TINYINT       NULL,
    [EWShowGuidelinesButton]           TINYINT       NULL,
    [EWShowSpellingButton]             TINYINT       NULL,
    [EWShowRemoveTextFormattingButton] TINYINT       NULL,
    [EWShowSuperScriptButton]          TINYINT       NULL,
    [EWShowSubScriptButton]            TINYINT       NULL,
    [EWDisableSourceMode]              TINYINT       NULL,
    [EWDisablePreviewMode]             TINYINT       NULL,
    [EWDisableImageUploading]          TINYINT       NULL,
    [EWDisableImageDeleting]           TINYINT       NULL,
    [EWDisableXHTMLFormatting]         TINYINT       NULL,
    [EWDisableSingleLineReturn]        TINYINT       NULL,
    [EWDisableInsertImageFromWeb]      TINYINT       NULL,
    [EWShowFlashButton]                TINYINT       NULL,
    [EWDisableFlashUploading]          TINYINT       NULL,
    [EWDisableFlashDeleting]           TINYINT       NULL,
    [EWDisableInsertFlashFromWeb]      TINYINT       NULL,
    [SessionTimeout]                   INT           NULL,
    [active]                           TINYINT       NULL,
    CONSTRAINT [PK_authors] PRIMARY KEY CLUSTERED ([authorID] ASC)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[authors] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[authors] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[authors] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[authors] TO [wfjuser]
    AS [dbo];



