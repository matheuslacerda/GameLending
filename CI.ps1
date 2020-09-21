docker-compose up -d
docker exec aspnet-core_game-lending_1 /migration/GameLending.DbMigrator.exe
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');