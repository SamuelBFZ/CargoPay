# CargoPay

This project is an APIRest for a payment company, composed mainly of 3 parts.

Pay Manager
User Manager
Card manager

For this it was used a .net 6.0 application with EF core along with an n-layer architecture (for this case the model is 4 layers), along with an authentication system based on a JWT Bearer Token.

The model is distributed as follows:

Presentation layer:
Controller Folder.

Business layer:
Service Folder. This layer stores the services together with their interfaces.

Data access layer:
Repository folder

Model layer:
DAL Folder. Here the entities are stored together with the database context.

There are also two extra folders, one called External Services where the external services such as UFE are stored. The other is Models, where the Enums used for User and payment status are stored.

## The key points of the project are as follows:

Endpoint Create Card: located in the card controller.
Endpoint Pay: located in the payment controller.
Endpoint Get Card Balance: located in the payment controller

The payment fee calculation logic is in the UFE service and is implemented in the Service for the Pay endpoint.

Multithreading methods using await and async were used as it was neccessary.

An authentication method based on JWT Bearer Token was used, upgrading security system that basic auth provide.

The implementation of transaction (Rollback, Complete, etc.) was not fully achieved, but it was with a simple and standardized ER model and good practices of the EF.

Cualquier duda o mejora necesaria en el codigo no duden en contactarme, enseñar es la forma mas facil de aprender :)
