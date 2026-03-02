STARTUP_FOLDER=./src/KatyFestas.API
INFRA_FOLDER=./src/KatyFestas.Infrastructure
TEST_FOLDER=./tests
SOLUTION=KatyFestas.sln

clean:
	dotnet clean $(SOLUTION)
	find . -type d -name 'bin' -exec rm -rf {} +
	find . -type d -name 'obj' -exec rm -rf {} +

restore:
	dotnet nuget locals all --clear
	dotnet restore $(SOLUTION)

build:
	dotnet build $(SOLUTION)

test:
	dotnet test $(SOLUTION)

run:
	dotnet run --project $(STARTUP_FOLDER)

clean_nuget:
	dotnet nuget locals all --clear

migrate:
	dotnet ef migrations add $(name) --startup-project $(STARTUP_FOLDER) --project $(INFRA_FOLDER)

remove_migration:
	dotnet ef migrations remove --startup-project $(STARTUP_FOLDER) --project $(INFRA_FOLDER)

update_migration:
	dotnet ef database update --startup-project $(STARTUP_FOLDER) --project $(INFRA_FOLDER)

revert_last_migration:
	dotnet ef database update $(name) --startup-project $(STARTUP_FOLDER) --project $(INFRA_FOLDER)

up_dev_db:
	docker compose up -d postgres

up_dev_cache:
	docker compose up -d redis

up_dev_api:
	docker compose up -d api

up_dev_all:
	docker compose up -d

down_dev:
	docker compose down

build_docker:
	docker build -t katyfestas_api -f $(STARTUP_FOLDER)/Dockerfile .

run_docker:
	docker run --name katyfestas_api -p 5000:8080 katyfestas_api

stop_docker:
	docker stop katyfestas_api
	docker rm katyfestas_api

restart_docker: stop_docker run_docker

logs_db:
	docker logs -f katyfestas_db

logs_cache:
	docker logs -f katyfestas_cache

logs_api:
	docker logs -f katyfestas_api
