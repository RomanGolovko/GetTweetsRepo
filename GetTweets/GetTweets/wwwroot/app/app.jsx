class Tweet extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.tweet };
    }

    render() {
        return <div>
            <p><b>{this.state.data.stamp}</b></p>
            <p>{this.state.data.text}</p>
        </div>;
    }
}

class Tweets extends React.Component {

    constructor(props) {
        super(props);
        this.state = { tweets: [] };
    }

    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ tweets: data });
        }.bind(this);
        xhr.send();
    }

    componentDidMount() {
        this.loadData();
    }

    render() {

        return <div>
            <h2>Tweets list</h2>
            {this.state.tweets.length == 0 &&
                <div>
                    <p>Loading...</p>
                    <p>It takes about 2-3 minutes due to big amount of data tries to get from external API</p>
                    <p>(13000 + tweets gets from external API then after removing duplicates 5000+ tweets send in one response here, in production it will be made by batches but this is test solution)</p>
                </div>
            }
            <div>
                {this.state.tweets.map((tweet) => {
                    return <Tweet key={tweet.id} tweet={tweet} />
                })
                }
            </div>
        </div>;
    }
}

ReactDOM.render(
    <Tweets apiUrl="/api/tweets/period?startdate=2016-01-01&enddate=2018-01-01" />,
    document.getElementById("content")
);