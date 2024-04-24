# Delivery Service

This service is part of an ASP.NET application that works with PostgreSQL. It handles courier/driver account and details management, as well as delivery assignment.

## Features

- Allow users (drivers) to register for new accounts.
- Allow users (drivers) to log in to existing accounts.
- Allow users (drivers) to set themselves as offline or online.
- Allow users (drivers) to view and accept delivery requests.
- Provide notifications regarding order status updates.


## Prerequisites

Before you begin, ensure you have the following installed:

- Docker: [Installation Guide](https://docs.docker.com/get-docker/)
- Postman: [Download Here](https://www.postman.com/downloads/)

## Getting Started

### Docker Compose

To run the project locally using Docker Compose, follow these steps:

1. Download docker-compose and postman collections [download](https://drive.google.com/drive/folders/1a-NlPiDkp8zquijd9lQY_L0PNoV_7Jsv?usp=sharing)
2. At The same docker-compose path run the following command:

```bash
docker-compose up
```
## Using Postman Collection

### Importing the Collection

1. **Download the Collection**: Insure that you download collection in previous link [download](https://drive.google.com/drive/folders/1a-NlPiDkp8zquijd9lQY_L0PNoV_7Jsv?usp=sharing).

2. **Import into Postman**: Open Postman, click on the "Import" button in the top left corner, and choose CusomerService.postman_collection.json. This will import the collection into your Postman workspace, reapte this for DeliveryService.postman_collection.json .

### Running the Collection

1. **Open Collection**: In Postman, navigate to the Collections tab on the left sidebar. Find and click on the imported collection to open it.

2. **Run Collection**: Click on the "Run" button located on the top right corner of the collection window. This will trigger the execution of all requests within the collection.

3. **View Results**: Postman will execute each request in the collection one by one and display the results in the Runner window. You can review the responses, check for errors, and inspect the test results.

3. **Run Individual Requests**: To run individual requests, navigate to the request you want to execute within the collection. Click on the request name to open it, then click on the "Send" button to send the request individually.

4. **View Examples**: Many requests within the collection may include examples. To view these examples, open the request and navigate to the "Examples" tab. Here you can see sample request and response payloads, headers, and other relevant information.
