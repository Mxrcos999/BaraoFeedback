FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Instala suporte a globalização
RUN apt-get update \
    && apt-get install -y --no-install-recommends libicu-dev \
    && rm -rf /var/lib/apt/lists/*

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY . .
