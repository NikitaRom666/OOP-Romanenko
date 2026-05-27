# Docker Guide
## Build
docker build -t myapp:v1 .
## Run
docker run --rm myapp:v1
## With volume
docker run --rm -v ./data:/app/data myapp:v1
## Compose
docker compose up --build
## Image sizes
- SDK: ~900MB
- Runtime: ~200MB
- Alpine: ~100MB
