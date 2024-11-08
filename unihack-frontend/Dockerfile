# frontend/Dockerfile

# Step 1: Build stage
FROM node:18 AS build
WORKDIR /app

# Copy package.json and install dependencies
COPY package*.json ./
RUN npm install

# Copy the rest of the application code and build the Angular app
COPY . .
RUN npm run build --prod

# Step 2: Runtime stage
FROM nginx:alpine
COPY --from=build /app/dist/unihack /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# No ENTRYPOINT needed; nginx runs by default
