# TollFee
Gothenburg City collects the Congestion tax which is charged during fixed hours for vehicles driving into and out of Gothenburg.

This is an API which calculates that toll fee for a number of vehicle passes.

## Gothenburg congestion tax toll rules
- https://www.transportstyrelsen.se/en/road/road-tolls/Congestion-taxes-in-Stockholm-and-Goteborg/congestion-tax-in-gothenburg/hours-and-amounts-in-gothenburg/

# Todo
- Make the free dates configurable runtime and stored persistently so that we can change depending on which year it is. If the request to "CalculateFee"-endpoint contains a year not yet configured, return relevant error message.
- Make sure business critical funcionality is covered by unit tests.
- Feel free to refactor/comment as you see fit as if it would be real business critical code, as long as existing endpoint's functionality doesn't change.