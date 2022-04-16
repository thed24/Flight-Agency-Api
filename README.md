# Flight-Agency Backend

This service is a clean architecture styled REST API that serves the needs of the Flight-Agency frontend. It allows users to manipulate the User object to track a users plans for their trips, and controls the authorization flow.

A few liberties have been taken for the sake of experimentation:

- The "domain logic" was pushed into an F# styled aggregate root, to play around with a functional approach to dealing with the CRUD aspect of things
  - This way, the application layer can orchestrate DB calls based on the resulting F# payload (because EFCore doesn't play nice with F#)
  - LanguageExt was used to make the interop between C# and F# more seamless
- DotNet 7 was used although it sits on the bleeding edge
- Tests were ignored because I wrote this and the front-end in my spare time and truthully, there's not a lot of logic that needs to be tested as of right now

## GCP

A `Dockerfile` exists for this project, which is used in conjunction with the `cloudbuilder.yaml` to facilitate continous deployment to GCPs `Cloud Build` on pushes to main, which will propogate to `Cloud Run` after the build succeeds.
