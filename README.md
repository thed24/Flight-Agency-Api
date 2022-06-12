# Flight-Agency Backend

This service is a clean architecture styled REST API that serves the needs of the Flight-Agency frontend. It allows users to manipulate the User object to track a users plans for their trips, and controls the authorization flow.

## GCP

A `Dockerfile` exists for this project, which is used in conjunction with the `cloudbuilder.yaml` to facilitate continous deployment to GCPs `Cloud Build` on pushes to main, which will propogate to `Cloud Run` after the build succeeds.
