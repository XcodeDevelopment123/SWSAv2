-- This SQL script marks all migrations as applied in the EFMigrationsHistory table
-- Run this in SQL Server on the Quartz database

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion)
VALUES 
('20250717052050_initial', '8.0.14'),
('20250721084942_update-system-audit-log', '8.0.14'),
('20250721090136_update-system-audit-log-remove-url', '8.0.14'),
('20250726161724_update-reminder', '8.0.14'),
('20250727050332_update-user-info', '8.0.14'),
('20250730030557_refactor-simplify-notification', '8.0.14'),
('20250814043356_remove-work-assignment', '8.0.14'),
('20250814055916_add-sec-dept-task-template', '8.0.14'),
('20250814060943_update-sec-dept-task-company', '8.0.14'),
('20250814065658_sec-strike-off-template', '8.0.14'),
('20250814074654_sec-strike-off-template-remark', '8.0.14'),
('20250815033930_add-audit-back-log', '8.0.14'),
('20250815034849_update-company-activity-size', '8.0.14'),
('20250815090115_add-audit-template', '8.0.14'),
('20250818011001_update-audit-template-word', '8.0.14'),
('20250818020519_update-audit-work-progress', '8.0.14'),
('20250819011223_update-aex-template-tax-strike-off', '8.0.14'),
('20251021071241_AddDecimalPrecision', '8.0.14'),
('20260612014937_AddGroupIdToClients', '8.0.14'),
('20260612020828_UpdateSchema', '8.0.14');
