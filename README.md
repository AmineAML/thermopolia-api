<h1 align="center">The Thermopolia API</h1>


<h4 align="center">An API for the Thermopolia App, a daily recipe recommendation and newsletter application.</h4>

<p align="center">
  <a href="#etymology">Etymology</a> •
  <a href="#key-features">Key Features</a> •
  <a href="#api">API</a> •
  <a href="#architecture-diagram">Architecture Diagram</a> •
  <!-- <a href="#license">License</a> -->
</p>

## Ethymology
Thermopolia (plural of thermopolium) was the ancient Roman/Greek equivanlt to restaurants. It was a commercial establishment for purshasing ready-to-eat food and often with servings similar to modern fast food. This name is a good description to what the application offers from easy-to-prepare recipes, newsletter subscriptions and delivery and other amazing features

## Key Features
The principal key features to keep note of are:
- Daily newsletter
- Weekly same ingredient recipes/drinks
- Week-ends recipes/drinks from a cuisine
- New recipes/drinks/diet each day
- Membres recipes/drinks articles

## API
Method|URL|Description
------|---|-----------
GET|/Recipes/foods|List ten random recipes
GET|/Recipes/foods/:id|Recipe by its id
GET|/Recipes/drinks|List then random drinks
GET|/Recipes/drinks/:id|Drink by its id
GET|/Recipes/diet|Diet randomly

## Architecture Diagram
![Thermopolia Architecture](./docs/diagrams/architecture.svg)   
C4 Model Diagram