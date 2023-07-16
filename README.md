# ElasticPOC

Notes:
* Before starting the project, type "docker-compose up -d" in base directory to fetch and run ElasticSearch and Kibana images from dockerhub.
* If you dont change ELKConfiguration.DeleteAndReseed parameter in appsettings.json, each time project starts, "product" index in ElasticSearch will be deleted and reseeded, so if you created any new data, it won't persist. 
