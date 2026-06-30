For elasticsearch and kibana installation:
1. install docker
2. open powershell
3. go to - cd ../ElasticsearchDemoCore2026/Infra/Docker
4. run - docker compose -p elasticsearchdemo up -d

elasticsearch server:
http://localhost:9200/

kibana:
http://localhost:5601/app/dev_tools#/console/shell

kibana query:

GET categories/_search
{
  "size": 1000,
  "query": {
    "match_all": {}
  }
}

GET products/_search
{
  "size": 1000,
  "query": {
    "match_all": {}
  }
}

===========================================================

For claude code:
update CLAUDE.md -
Please review the entire project and update CLAUDE.md wherever necessary to reflect the current implementation, architecture, and any new patterns or changes introduced in the codebase.