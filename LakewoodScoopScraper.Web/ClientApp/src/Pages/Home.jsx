import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Home = () => {

    const [newsItems, setNewsItems] = useState([]);

    useEffect(() => {
        const getNewsItems = async () => {
            const { data } = await axios.get('/api/newsitemscrape/scrapenews');
            setNewsItems(data);
        }

        getNewsItems();
    }, []);

    return (
        <div className="container" style={{ marginTop: 80 }}>
            {!newsItems.length && <h1>There are no news currently available</h1>}
            {!!newsItems.length && <div>
                <table className='table table-bordered'>
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Title</th>
                        </tr>
                    </thead>
                    <tbody>
                        {newsItems.map(news => (
                            <tr key={news.url}>
                                <td>
                                    <img src={news.image} style={{ height: 150 }} alt={news.title} className='img=thumbnail'></img>
                                </td>
                                <td>
                                    <a href={news.url} target='_blank'>{news.title}</a>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>}
        </div>
    );
};

export default Home;