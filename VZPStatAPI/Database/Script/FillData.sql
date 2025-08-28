USE [ke_call250]
GO
SET IDENTITY_INSERT [statistics].[Diagnostic] ON 
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (1, N'Nulování fronty')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (2, N'Mazání událostí')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (3, N'Změna konfigurace')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (4, N'Alarm')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (5, N'Volání vedoucího')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (6, N'Zadání PINu nebo karty')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (7, N'Automatické nulování fronty při změně dat')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (8, N'Nulování fronty konfiguračním klientem')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (9, N'Přenos konfigurace z konfiguračního klienta')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (10, N'Chyba komunikace')
GO
INSERT [statistics].[Diagnostic] ([DiagnosticId], [Description]) VALUES (11, N'Vývoj')
GO
SET IDENTITY_INSERT [statistics].[Diagnostic] OFF
GO
SET IDENTITY_INSERT [statistics].[EventName] ON 
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (1, N'Stisk tlačítka, zařazení k činnosti')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (2, N'Stisk tlač., zařazení k činnosti s prioritou')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (3, N'Stisk tlačítka, zařazení k přepážce')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (4, N'Stisk tlačítka, zařazení k přepážce s prioritou')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (5, N'Zařazení k činnosti z přepážky')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (6, N'Zařazení k činnosti z přepážky s prioritou')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (7, N'Vyvolání klienta')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (8, N'Vyvolání klienta mimo frontu')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (9, N'Ukončení obsluhy')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (10, N'Přeložení k přepážce')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (11, N'Přeložení k činnosti')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (12, N'Přeložení k obsluhujícímu')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (13, N'Přihlášení obsluhujícího na přep.')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (14, N'Odhlášení obsluhujícího z přep.')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (15, N'Přihlášení přepážky do systému')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (16, N'Odhlášení přepážky ze systému')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (17, N'Změna stavu tiskárny')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (18, N'Vyvolání klienta mimo pořadí')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (19, N'Opakované vyvolání klienta')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (20, N'Deaktivace systému')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (21, N'Aktivace systému')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (22, N'Diagnostika-nulování fronty')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (23, N'Diagnostika-mazání událostí')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (24, N'Diagnostika-změna konfigurace')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (25, N'Diagnostika-alarm')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (26, N'Diagnostika-volání vedoucího')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (27, N'Diagnostika-zadání PINu nebo karty')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (28, N'Diagnostika-automatické nulování fronty při změně data')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (29, N'Diagnostika-nulování fronty konfiguračním klientem')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (30, N'Diagnostika-přenos konfigurace z konfiguračního klienta')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (31, N'Diagnostika-Chyba komunikace')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (32, N'Diagnostika-vývoj')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (33, N'První paket rozšířených dat')
GO
INSERT [statistics].[EventName] ([EventNameId], [Name]) VALUES (34, N'Další paket rozšířených dat')
GO
SET IDENTITY_INSERT [statistics].[EventName] OFF
GO
SET IDENTITY_INSERT [statistics].[PacketDataType] ON 
GO
INSERT [statistics].[PacketDataType] ([ExtendedDataTypeId], [Description]) VALUES (1, N'Jméno klienta')
GO
INSERT [statistics].[PacketDataType] ([ExtendedDataTypeId], [Description]) VALUES (2, N'Platný kód')
GO
INSERT [statistics].[PacketDataType] ([ExtendedDataTypeId], [Description]) VALUES (3, N'Neplatný kód')
GO
INSERT [statistics].[PacketDataType] ([ExtendedDataTypeId], [Description]) VALUES (4, N'Platná karta')
GO
INSERT [statistics].[PacketDataType] ([ExtendedDataTypeId], [Description]) VALUES (5, N'Neplatná karta')
GO
INSERT [statistics].[PacketDataType] ([ExtendedDataTypeId], [Description]) VALUES (6, N'Poznámka')
GO
SET IDENTITY_INSERT [statistics].[PacketDataType] OFF
GO
SET IDENTITY_INSERT [statistics].[PeriphType] ON 
GO
INSERT [statistics].[PeriphType] ([PerihTypeId], [Description]) VALUES (1, N'Přepážka')
GO
INSERT [statistics].[PeriphType] ([PerihTypeId], [Description]) VALUES (2, N'Klient')
GO
INSERT [statistics].[PeriphType] ([PerihTypeId], [Description]) VALUES (3, N'Displej')
GO
INSERT [statistics].[PeriphType] ([PerihTypeId], [Description]) VALUES (4, N'Tiskárna/Ts')
GO
SET IDENTITY_INSERT [statistics].[PeriphType] OFF
GO
SET IDENTITY_INSERT [statistics].[PrinterCurrentStatus] ON 
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (1, N'Ok')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (2, N'Offline')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (3, N'Došel papír')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (4, N'Otevřené dveře mincovníku')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (5, N'Chyba počítadla mincovníku')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (6, N'Zaseklá mince')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (7, N'Neautorizované otevření dveří mincovníku')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (8, N'Mincovník nepřipojen')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (9, N'Obecná chyba tiskárny')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (10, N'Tiskárna odpojena')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (11, N'PA servis akceptoru mincí')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (12, N'PA servis akc. bankovek')
GO
INSERT [statistics].[PrinterCurrentStatus] ([PrinterCurrentStatusId], [Description]) VALUES (13, N'PA chyba akceptoru bankovek')
GO
SET IDENTITY_INSERT [statistics].[PrinterCurrentStatus] OFF
GO
SET IDENTITY_INSERT [statistics].[PrinterPreviousStatus] ON 
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (1, N'Ok')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (2, N'Offline')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (3, N'Došel papír')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (4, N'Otevřené dveře mincovníku')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (5, N'Chyba počítadla mincovníku')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (6, N'Zaseklá mince')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (7, N'Neautorizované otevření dveří mincovníku')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (8, N'Mincovník nepřipojen')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (9, N'Obecná chyba tiskárny')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (10, N'Tiskárna odpojena')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (11, N'PA servis akceptoru mincí')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (12, N'PA servis akc. bankovek')
GO
INSERT [statistics].[PrinterPreviousStatus] ([PrinterPreviousStatusId], [Description]) VALUES (13, N'PA chyba akceptoru bankovek')
GO
SET IDENTITY_INSERT [statistics].[PrinterPreviousStatus] OFF
GO
SET IDENTITY_INSERT [statistics].[Reason] ON 
GO
INSERT [statistics].[Reason] ([ReasonId], [Description]) VALUES (1, N'Klient se nedostavil')
GO
INSERT [statistics].[Reason] ([ReasonId], [Description]) VALUES (2, N'Smazán z fronty')
GO
INSERT [statistics].[Reason] ([ReasonId], [Description]) VALUES (3, N'Zpracování klienta')
GO
SET IDENTITY_INSERT [statistics].[Reason] OFF
GO
SET IDENTITY_INSERT [statistics].[TransferReason] ON
GO
INSERT [statistics].[TransferReason] ([TransferReasonId], [Description]) VALUES (1, N'Priorita')
GO
INSERT [statistics].[TransferReason] ([TransferReasonId], [Description]) VALUES (2, N'Přeložení na čas')
GO
INSERT [statistics].[TransferReason] ([TransferReasonId], [Description]) VALUES (3, N'Zpracování klienta')
GO
SET IDENTITY_INSERT [statistics].[TransferReason] OFF
GO
SET IDENTITY_INSERT [statistics].[ClientStatus] ON 
GO
INSERT [statistics].[ClientStatus] ([ClientStatusId], [Status]) VALUES (1, N'Cekani ve fronte')
GO
INSERT [statistics].[ClientStatus] ([ClientStatusId], [Status]) VALUES (2, N'Obsluha klienta')
GO
INSERT [statistics].[ClientStatus] ([ClientStatusId], [Status]) VALUES (3, N'Ukonceni obsluhy')
GO
SET IDENTITY_INSERT [statistics].[ClientStatus] OFF
GO
SET IDENTITY_INSERT [statistics].[ClerkStatus] ON 
GO
INSERT [statistics].[ClerkStatus] ([ClerkStatusId], [Status]) VALUES (1, N'Přihlášený')
GO
INSERT [statistics].[ClerkStatus] ([ClerkStatusId], [Status]) VALUES (2, N'Přestávka')
GO
INSERT [statistics].[ClerkStatus] ([ClerkStatusId], [Status]) VALUES (3, N'Back office')
GO
INSERT [statistics].[ClerkStatus] ([ClerkStatusId], [Status]) VALUES (4, N'Odhlášený')
GO
SET IDENTITY_INSERT [statistics].[ClerkStatus] OFF
GO
SET IDENTITY_INSERT [statistics].[CounterStatus] ON 
GO
INSERT [statistics].[CounterStatus] ([CounterStatusId], [Status]) VALUES (1, N'Aktivní přihlášení FO pracovníka')
GO
INSERT [statistics].[CounterStatus] ([CounterStatusId], [Status]) VALUES (2, N'Přestávka FO')
GO
INSERT [statistics].[CounterStatus] ([CounterStatusId], [Status]) VALUES (3, N'Back office FO pracovníka')
GO
INSERT [statistics].[CounterStatus] ([CounterStatusId], [Status]) VALUES (4, N'Přepážka neobsazena FO pracovníkem')
GO
SET IDENTITY_INSERT [statistics].[CounterStatus] OFF
GO
SET IDENTITY_INSERT [statistics].[Region] ON 
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (0, N'PRAHA-ÚSTŘ.POJIŠŤOVNA') 
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (1, N'Krajská pobočka pro hl. m. Prahu')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (2, N'Krajská pobočka pro Středočeský kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (3, N'Krajská pobočka pro Jihočeský kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (4, N'Krajská pobočka pro Plzeňský kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (5, N'Krajská pobočka pro Karlovarský kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (6, N'Krajská pobočka pro Ústecký kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (7, N'Krajská pobočka pro Liberecký kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (8, N'Krajská pobočka pro Královéhradecký kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (9, N'Krajská pobočka pro Pardubický kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (10, N'Krajská pobočka pro kraj Vysočina')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (11, N'Krajská pobočka pro Jihomoravský kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (12, N'Krajská pobočka pro Olomoucký kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (13, N'Krajská pobočka pro Moravskoslezský kraj')
GO
INSERT [statistics].[Region] ([RegionId], [RegionName]) VALUES (14, N'Krajská pobočka pro Zlínský kraj')
GO
SET IDENTITY_INSERT [statistics].[Region] OFF
GO
SET IDENTITY_INSERT [statistics].[Branch] ON 
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (1, 0, CAST(N'2022-12-07T19:59:06.9071887' AS DateTime2), NULL, 0, N'0100', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (2, 1, CAST(N'2022-11-15T15:07:10.3562611' AS DateTime2), NULL, 0, N'0200', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (3, 1, CAST(N'2022-11-15T15:08:37.0953089' AS DateTime2), NULL, 0, N'0300', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (4, 1, CAST(N'2022-11-15T15:08:37.0953186' AS DateTime2), NULL, 0, N'0400', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (5, 1, CAST(N'2022-11-15T15:08:37.0953192' AS DateTime2), NULL, 0, N'0500', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (6, 1, CAST(N'2022-11-15T15:08:37.0953198' AS DateTime2), NULL, 0, N'0600', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (7, 1, CAST(N'2022-11-15T15:08:37.0953209' AS DateTime2), NULL, 0, N'0700', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (8, 1, CAST(N'2022-11-15T15:08:37.0953214' AS DateTime2), NULL, 0, N'0800', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (9, 1, CAST(N'2022-11-15T15:08:37.0953271' AS DateTime2), NULL, 0, N'0900', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (10, 1, CAST(N'2022-11-15T15:08:37.0953277' AS DateTime2), NULL, 0, N'1000', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (11, 1, CAST(N'2022-11-15T15:08:37.0953282' AS DateTime2), NULL, 0, N'1400', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (12, 1, CAST(N'2022-11-15T15:08:37.0953286' AS DateTime2), NULL, 0, N'1900', N'Krajská pobočka pro hl. m. Prahu', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (13, 0, CAST(N'2022-12-07T19:59:09.9985976' AS DateTime2), NULL, 0, N'2000', N'Územní pracoviště BENEŠOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (14, 2, CAST(N'2022-11-15T15:14:59.4886328' AS DateTime2), NULL, 0, N'2100', N'Územní pracoviště BEROUN', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (15, 0, CAST(N'2022-12-07T19:59:14.1569694' AS DateTime2), NULL, 0, N'2200', N'Územní pracoviště KLADNO', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (16, 2, CAST(N'2022-11-15T15:14:59.4886361' AS DateTime2), NULL, 0, N'2300', N'Územní pracoviště KOLÍN', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (17, 2, CAST(N'2022-11-15T15:14:59.4886367' AS DateTime2), NULL, 0, N'2400', N'Územní pracoviště KUTNÁ HORA', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (18, 2, CAST(N'2022-11-15T15:14:59.4886372' AS DateTime2), NULL, 0, N'2500', N'Územní pracoviště MĚLNÍK', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (19, 2, CAST(N'2022-11-15T15:14:59.4886377' AS DateTime2), NULL, 0, N'2600', N'Územní pracoviště MLADÁ BOLESLAV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (20, 2, CAST(N'2022-11-15T15:14:59.4886382' AS DateTime2), NULL, 0, N'2700', N'Územní pracoviště NYMBURK', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (21, 2, CAST(N'2022-11-15T15:14:59.4886387' AS DateTime2), NULL, 0, N'2800', N'Územní pracoviště PRAHA-VÝCHOD', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (22, 2, CAST(N'2022-11-15T15:14:59.4886449' AS DateTime2), NULL, 0, N'2900', N'Územní pracoviště PRAHA-ZÁPAD', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (23, 2, CAST(N'2022-11-15T15:14:59.4886455' AS DateTime2), NULL, 0, N'3000', N'Územní pracoviště PŘÍBRAM', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (24, 2, CAST(N'2022-11-15T15:14:59.4886460' AS DateTime2), NULL, 0, N'3100', N'Územní pracoviště RAKOVNÍK', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (25, 3, CAST(N'2022-11-15T15:14:59.4886465' AS DateTime2), NULL, 0, N'3200', N'Územní pracoviště ČESKÉ BUDĚJOVICE', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (26, 3, CAST(N'2022-11-15T15:14:59.4886470' AS DateTime2), NULL, 0, N'3300', N'Územní pracoviště ČESKÝ KRUMLOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (27, 3, CAST(N'2022-11-15T15:14:59.4886475' AS DateTime2), NULL, 0, N'3400', N'Územní pracoviště JINDŘICHŮV HRADEC', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (28, 10, CAST(N'2022-11-15T15:19:18.4255561' AS DateTime2), NULL, 0, N'3500', N'Územní pracoviště PELHŘIMOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (29, 3, CAST(N'2022-11-15T15:19:18.4255744' AS DateTime2), NULL, 0, N'3600', N'Územní pracoviště PÍSEK', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (30, 3, CAST(N'2022-11-15T15:19:18.4255750' AS DateTime2), NULL, 0, N'3700', N'Územní pracoviště PRACHATICE', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (31, 3, CAST(N'2022-11-15T15:19:18.4255754' AS DateTime2), NULL, 0, N'3800', N'Územní pracoviště STRAKONICE', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (32, 3, CAST(N'2022-11-15T15:19:18.4255758' AS DateTime2), NULL, 0, N'3900', N'Územní pracoviště TÁBOR', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (33, 4, CAST(N'2022-11-15T15:19:18.4255763' AS DateTime2), NULL, 0, N'4000', N'Územní pracoviště DOMAŽLICE', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (34, 5, CAST(N'2022-11-15T15:30:16.8962859' AS DateTime2), NULL, 0, N'4100', N'Územní pracoviště CHEB', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (35, 5, CAST(N'2022-11-15T15:30:16.8962973' AS DateTime2), NULL, 0, N'4200', N'Územní pracoviště KARLOVY VARY', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (36, 4, CAST(N'2022-11-15T15:30:16.8962978' AS DateTime2), NULL, 0, N'4300', N'Územní pracoviště KLATOVY', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (37, 4, CAST(N'2022-11-15T15:30:16.8962982' AS DateTime2), NULL, 0, N'4400', N'Územní pracoviště PLZEŇ-MĚSTO', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (38, 4, CAST(N'2022-11-15T15:30:16.8962987' AS DateTime2), NULL, 0, N'4500', N'Územní pracoviště PLZEŇ-JIH', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (39, 4, CAST(N'2022-11-15T15:30:16.8962991' AS DateTime2), NULL, 0, N'4600', N'Územní pracoviště PLZEŇ-SEVER', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (40, 4, CAST(N'2022-11-15T15:30:16.8962996' AS DateTime2), NULL, 0, N'4700', N'Územní pracoviště ROKYCANY', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (41, 5, CAST(N'2022-11-15T15:30:16.8963000' AS DateTime2), NULL, 0, N'4800', N'Územní pracoviště SOKOLOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (42, 4, CAST(N'2022-11-15T15:30:16.8963017' AS DateTime2), NULL, 0, N'4900', N'Územní pracoviště TACHOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (43, 7, CAST(N'2022-11-15T15:30:16.8963021' AS DateTime2), NULL, 0, N'5000', N'Územní pracoviště ČESKÁ LÍPA', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (44, 6, CAST(N'2022-11-15T15:34:19.3026355' AS DateTime2), NULL, 0, N'5100', N'Územní pracoviště DĚČÍN', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (45, 6, CAST(N'2022-11-15T15:34:19.3026416' AS DateTime2), NULL, 0, N'5200', N'Územní pracoviště CHOMUTOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (46, 7, CAST(N'2022-11-15T15:34:19.3026432' AS DateTime2), NULL, 0, N'5300', N'Územní pracoviště JABLONEC NAD NISOU', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (47, 7, CAST(N'2022-11-15T15:34:19.3026437' AS DateTime2), NULL, 0, N'5400', N'Územní pracoviště LIBEREC', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (48, 6, CAST(N'2022-11-15T15:34:19.3026441' AS DateTime2), NULL, 0, N'5500', N'Územní pracoviště LITOMĚŘICE', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (49, 6, CAST(N'2022-11-15T15:34:19.3026446' AS DateTime2), NULL, 0, N'5600', N'Územní pracoviště LOUNY', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (50, 6, CAST(N'2022-11-15T15:34:19.3026451' AS DateTime2), NULL, 0, N'5700', N'Územní pracoviště MOST', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (51, 6, CAST(N'2022-11-15T15:34:19.3026455' AS DateTime2), NULL, 0, N'5800', N'Územní pracoviště TEPLICE', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (52, 6, CAST(N'2022-11-15T15:34:19.3026460' AS DateTime2), NULL, 0, N'5900', N'Územní pracoviště ÚSTÍ NAD LABEM', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (53, 10, CAST(N'2022-11-15T15:34:19.3026464' AS DateTime2), NULL, 0, N'6000', N'Územní pracoviště HAVLÍČKŮV BROD', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (54, 8, CAST(N'2022-11-15T15:34:19.3026478' AS DateTime2), NULL, 0, N'6100', N'Územní pracoviště HRADEC KRÁLOVÉ', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (55, 9, CAST(N'2022-11-15T15:42:09.9672611' AS DateTime2), NULL, 0, N'6200', N'Územní pracoviště CHRUDIM', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (56, 8, CAST(N'2022-11-15T15:42:09.9672723' AS DateTime2), NULL, 0, N'6300', N'Územní pracoviště JIČÍN', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (57, 8, CAST(N'2022-11-15T15:42:09.9672728' AS DateTime2), NULL, 0, N'6400', N'Územní pracoviště NÁCHOD', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (58, 9, CAST(N'2022-11-15T15:42:09.9672733' AS DateTime2), NULL, 0, N'6500', N'Územní pracoviště PARDUBICE', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (59, 8, CAST(N'2022-11-15T15:42:09.9672737' AS DateTime2), NULL, 0, N'6600', N'Územní pracoviště RYCHNOV NAD KNĚŽNOU', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (60, 7, CAST(N'2022-11-15T15:42:09.9672741' AS DateTime2), NULL, 0, N'6700', N'Územní pracoviště SEMILY', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (61, 9, CAST(N'2022-11-15T15:42:09.9672746' AS DateTime2), NULL, 0, N'6800', N'Územní pracoviště SVITAVY', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (62, 8, CAST(N'2022-11-15T15:42:09.9672762' AS DateTime2), NULL, 0, N'6900', N'Územní pracoviště TRUTNOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (63, 9, CAST(N'2022-11-15T15:42:09.9672768' AS DateTime2), NULL, 0, N'7000', N'Územní pracoviště ÚSTÍ NAD ORLICÍ', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (64, 11, CAST(N'2022-11-15T15:42:09.9672783' AS DateTime2), NULL, 0, N'7100', N'Územní pracoviště BLANSKO', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (65, 11, CAST(N'2022-11-15T15:42:09.9672788' AS DateTime2), NULL, 0, N'7200', N'Územní pracoviště BRNO-MĚSTO', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (66, 11, CAST(N'2022-11-15T15:42:09.9672793' AS DateTime2), NULL, 0, N'7300', N'Územní pracoviště BRNO-VENKOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (67, 11, CAST(N'2022-11-15T15:42:09.9672798' AS DateTime2), NULL, 0, N'7400', N'Územní pracoviště BŘECLAV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (68, 11, CAST(N'2022-11-15T15:42:09.9672803' AS DateTime2), NULL, 0, N'7500', N'Územní pracoviště HODONÍN', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (69, 10, CAST(N'2022-11-15T15:42:09.9672808' AS DateTime2), NULL, 0, N'7600', N'Územní pracoviště JIHLAVA', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (70, 14, CAST(N'2022-11-15T15:42:09.9672812' AS DateTime2), NULL, 0, N'7700', N'Územní pracoviště KROMĚŘÍŽ', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (71, 12, CAST(N'2022-11-15T15:42:09.9672828' AS DateTime2), NULL, 0, N'7800', N'Územní pracoviště PROSTĚJOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (72, 10, CAST(N'2022-11-15T15:42:09.9672832' AS DateTime2), NULL, 0, N'7900', N'Územní pracoviště TŘEBÍČ', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (73, 14, CAST(N'2022-11-15T15:42:09.9672836' AS DateTime2), NULL, 0, N'8000', N'Územní pracoviště UHERSKÉ HRADIŠTĚ', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (74, 11, CAST(N'2022-11-15T15:51:19.7647513' AS DateTime2), NULL, 0, N'8100', N'Územní pracoviště VYŠKOV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (75, 14, CAST(N'2022-11-15T15:51:19.7647648' AS DateTime2), NULL, 0, N'8200', N'Územní pracoviště ZLÍN', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (76, 11, CAST(N'2022-11-15T15:51:19.7647653' AS DateTime2), NULL, 0, N'8300', N'Územní pracoviště ZNOJMO', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (77, 10, CAST(N'2022-11-15T15:51:19.7647658' AS DateTime2), NULL, 0, N'8400', N'Územní pracoviště ŽĎÁR NAD SÁZAVOU', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (78, 13, CAST(N'2022-11-15T15:51:19.7647662' AS DateTime2), NULL, 0, N'8500', N'Územní pracoviště OPAVA, úřad. Bruntál', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (79, 13, CAST(N'2022-11-15T15:51:19.7647666' AS DateTime2), NULL, 0, N'8600', N'Územní pracoviště FRÝDEK-MÍSTEK', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (80, 13, CAST(N'2022-11-15T15:51:19.7647670' AS DateTime2), NULL, 0, N'8700', N'Územní pracoviště KARVINÁ', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (81, 13, CAST(N'2022-11-15T15:51:19.7647675' AS DateTime2), NULL, 0, N'8800', N'Územní pracoviště NOVÝ JIČÍN', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (82, 12, CAST(N'2022-11-15T15:51:19.7647679' AS DateTime2), NULL, 0, N'8900', N'Územní pracoviště OLOMOUC', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (83, 13, CAST(N'2022-11-15T15:51:19.7647720' AS DateTime2), NULL, 0, N'9000', N'Územní pracoviště OPAVA', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (84, 13, CAST(N'2022-11-15T15:51:19.7647725' AS DateTime2), NULL, 0, N'9100', N'Územní pracoviště OSTRAVA', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (85, 12, CAST(N'2022-11-15T15:51:19.7647729' AS DateTime2), NULL, 0, N'9200', N'Územní pracoviště PŘEROV', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (86, 12, CAST(N'2022-11-15T15:51:19.7647734' AS DateTime2), NULL, 0, N'9300', N'Územní pracoviště ŠUMPERK', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (87, 14, CAST(N'2022-11-15T15:51:19.7647738' AS DateTime2), NULL, 0, N'9400', N'Územní pracoviště VSETÍN', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (88, 12, CAST(N'2022-11-15T15:51:19.7647742' AS DateTime2), NULL, 0, N'9500', N'Územní pracoviště ŠUMPERK, úřad. Jeseník', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (89, 0, CAST(N'2022-11-23T12:15:30.3202005' AS DateTime2), NULL, 0, N'9800', N'Územní pracoviště ÚP VZP', N'string', N'string')
GO
INSERT [statistics].[Branch] ([BranchId], [RegionId], [CreatedDate], [DeletedDate], [Online], [VZP_code], [BranchName], [IpAddress], [Port]) VALUES (90, 0, CAST(N'2022-11-23T12:15:46.8308922' AS DateTime2), NULL, 0, N'9900', N'Územní pracoviště PRAHA-ÚSTŘ.POJIŠŤOVNA', N'string', N'string')
GO
SET IDENTITY_INSERT [statistics].[Branch] OFF
GO
SET IDENTITY_INSERT [app].[Role] ON 
GO
INSERT [app].[Role] ([RoleId], [CreatedDate], [DeletedDate], [Name]) VALUES (1, CAST(N'2022-11-16T11:11:55.8333333' AS DateTime2), NULL, N'Administrator')
GO
INSERT [app].[Role] ([RoleId], [CreatedDate], [DeletedDate], [Name]) VALUES (2, CAST(N'2022-11-16T11:14:12.9200000' AS DateTime2), NULL, N'Manazer')
GO
INSERT [app].[Role] ([RoleId], [CreatedDate], [DeletedDate], [Name]) VALUES (3, CAST(N'2022-11-16T11:14:12.9200000' AS DateTime2), NULL, N'Marketingovy_manazer')
GO
INSERT [app].[Role] ([RoleId], [CreatedDate], [DeletedDate], [Name]) VALUES (4, CAST(N'2022-11-16T11:14:12.9200000' AS DateTime2), NULL, N'Frontoffice_pracovnik')
GO
SET IDENTITY_INSERT [app].[Role] OFF
GO
SET IDENTITY_INSERT [app].[App] ON 
GO
INSERT [app].[App] ([AppId], [CreatedDate], [DeletedDate], [Name], [ShowBranches]) VALUES (101, CAST(N'2022-11-16T14:01:19.7366667' AS DateTime2), NULL, N'Administrace, celkový přehled', 0)
GO
INSERT [app].[App] ([AppId], [CreatedDate], [DeletedDate], [Name], [ShowBranches]) VALUES (102, CAST(N'2022-11-16T14:04:47.8166667' AS DateTime2), NULL, N'Konfigurace pobočky', 1)
GO
INSERT [app].[App] ([AppId], [CreatedDate], [DeletedDate], [Name], [ShowBranches]) VALUES (103, CAST(N'2022-11-16T14:05:08.9500000' AS DateTime2), NULL, N'Konfigurace dotekových obrazovek', 0)
GO
INSERT [app].[App] ([AppId], [CreatedDate], [DeletedDate], [Name], [ShowBranches]) VALUES (104, CAST(N'2022-11-16T14:05:26.9100000' AS DateTime2), NULL, N'Marketingový obsah TV displejů', 0)
GO
INSERT [app].[App] ([AppId], [CreatedDate], [DeletedDate], [Name], [ShowBranches]) VALUES (105, CAST(N'2022-11-16T14:05:47.5133333' AS DateTime2), NULL, N'Obsluha přepážky', 1)
GO
INSERT [app].[App] ([AppId], [CreatedDate], [DeletedDate], [Name], [ShowBranches]) VALUES (106, CAST(N'2022-11-16T14:06:04.2500000' AS DateTime2), NULL, N'Statistika', 0)
GO
INSERT [app].[App] ([AppId], [CreatedDate], [DeletedDate], [Name], [ShowBranches]) VALUES (107, CAST(N'2022-11-16T14:06:44.0100000' AS DateTime2), NULL, N'Konfigurace rezerv. systému (WebCall)', 0)
GO
SET IDENTITY_INSERT [app].[App] OFF
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (102, 1)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (103, 1)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (104, 1)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (105, 1)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (106, 1)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (107, 1)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (102, 2)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (103, 2)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (104, 2)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (105, 2)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (106, 2)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (107, 2)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (104, 3)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (105, 4)
GO
INSERT [app].[AppRole] ([AppsAppId], [RolesRoleId]) VALUES (106, 4)
GO
SET IDENTITY_INSERT [app].[User] ON 
GO
INSERT [app].[User] ([UserId], [CreatedDate], [DeletedDate], [Name], [Login]) VALUES (1, CAST(N'2022-11-16T14:18:44.1933333' AS DateTime2), NULL, N'Uživatel - administrátor', N'admin@vzp.cz')
GO
INSERT [app].[User] ([UserId], [CreatedDate], [DeletedDate], [Name], [Login]) VALUES (2, CAST(N'2022-11-16T14:22:26.2466667' AS DateTime2), NULL, N'Uživatel - manažér', N'manager@vzp.cz')
GO
INSERT [app].[User] ([UserId], [CreatedDate], [DeletedDate], [Name], [Login]) VALUES (3, CAST(N'2022-11-16T14:22:26.2466667' AS DateTime2), NULL, N'Uživatel - marketér', N'marketing@vzp.cz')
GO
INSERT [app].[User] ([UserId], [CreatedDate], [DeletedDate], [Name], [Login]) VALUES (4, CAST(N'2022-11-16T14:22:26.2466667' AS DateTime2), NULL, N'Uživatel - obsluhující 1', N'clerk1@vzp.cz')
GO
INSERT [app].[User] ([UserId], [CreatedDate], [DeletedDate], [Name], [Login]) VALUES (5, CAST(N'2022-11-16T14:22:26.2466667' AS DateTime2), NULL, N'Uživatel - obsluhující 2', N'clerk2@vzp.cz')
GO
INSERT [app].[User] ([UserId], [CreatedDate], [DeletedDate], [Name], [Login]) VALUES (6, CAST(N'2022-11-16T14:22:26.2466667' AS DateTime2), NULL, N'Tomáš Drtílek', N'drtilek@kadlecelektro.cz')
GO
SET IDENTITY_INSERT [app].[User] OFF
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (1, 1)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (3, 1)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (4, 1)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (2, 2)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (3, 2)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (4, 2)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (3, 3)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (4, 4)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (4, 5)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (1, 6)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (3, 6)
GO
INSERT [app].[RoleUser] ([RolesRoleId], [UsersUserId]) VALUES (4, 6)
GO
