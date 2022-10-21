# Trading

<!-- ABOUT THE PROJECT -->
## About The Project
Yet another trading bot 😄

The main idea of the project is extremely simple:  

start trading session -> listen to candle updates -> check for signals -> filter them by neural network based filter 
-> handle new signal (various behaviour that depends on session type, session state, instrument open positon availability e.t.c)

The bot is in prototype development stage, of course it ain't a production version, not even close 😏.
But you can already see the way how I decomposing the subject area, using abstractions, building architecture and actually writing code


<!-- ROADMAP -->
## MVP Roadmap

- [ ] Add exchange market simulation
- [ ] Add backtest
- [ ] Add virtual positions
- [ ] Add strategy analytics
- [ ] Add signals filter based on neural network
- [ ] Add exchange socket unexpected disconnect handling mechanism
- [ ] Add realtime trading
