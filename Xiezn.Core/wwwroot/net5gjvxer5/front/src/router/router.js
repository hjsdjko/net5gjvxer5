import VueRouter from 'vue-router'

//引入组件
import Index from '../pages'
import Home from '../pages/home/home'
import Login from '../pages/login/login'
import Register from '../pages/register/register'
import Center from '../pages/center/center'
import Forum from '../pages/forum/list'
import ForumAdd from '../pages/forum/add'
import ForumDetail from '../pages/forum/detail'
import MyForumList from '../pages/forum/myForumList'
import Storeup from '../pages/storeup/list'
import News from '../pages/news/news-list'
import NewsDetail from '../pages/news/news-detail'
import payList from '../pages/pay'

import yonghuList from '../pages/yonghu/list'
import yonghuDetail from '../pages/yonghu/detail'
import yonghuAdd from '../pages/yonghu/add'
import shipinbiaoqianList from '../pages/shipinbiaoqian/list'
import shipinbiaoqianDetail from '../pages/shipinbiaoqian/detail'
import shipinbiaoqianAdd from '../pages/shipinbiaoqian/add'
import shipinfenleiList from '../pages/shipinfenlei/list'
import shipinfenleiDetail from '../pages/shipinfenlei/detail'
import shipinfenleiAdd from '../pages/shipinfenlei/add'
import shipindianboList from '../pages/shipindianbo/list'
import shipindianboDetail from '../pages/shipindianbo/detail'
import shipindianboAdd from '../pages/shipindianbo/add'
import newstypeList from '../pages/newstype/list'
import newstypeDetail from '../pages/newstype/detail'
import newstypeAdd from '../pages/newstype/add'
import aboutusList from '../pages/aboutus/list'
import aboutusDetail from '../pages/aboutus/detail'
import aboutusAdd from '../pages/aboutus/add'
import systemintroList from '../pages/systemintro/list'
import systemintroDetail from '../pages/systemintro/detail'
import systemintroAdd from '../pages/systemintro/add'
import friendlinkList from '../pages/friendlink/list'
import friendlinkDetail from '../pages/friendlink/detail'
import friendlinkAdd from '../pages/friendlink/add'
import onlinemessageList from '../pages/onlinemessage/list'
import onlinemessageDetail from '../pages/onlinemessage/detail'
import onlinemessageAdd from '../pages/onlinemessage/add'
import discussshipindianboList from '../pages/discussshipindianbo/list'
import discussshipindianboDetail from '../pages/discussshipindianbo/detail'
import discussshipindianboAdd from '../pages/discussshipindianbo/add'

const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(location) {
	return originalPush.call(this, location).catch(err => err)
}

//配置路由
export default new VueRouter({
	routes:[
		{
      path: '/',
      redirect: '/index/home'
    },
		{
			path: '/index',
			component: Index,
			children:[
				{
					path: 'home',
					component: Home
				},
				{
					path: 'center',
					component: Center,
				},
				{
					path: 'pay',
					component: payList,
				},
				{
					path: 'forum',
					component: Forum
				},
				{
					path: 'forumAdd',
					component: ForumAdd
				},
				{
					path: 'forumDetail',
					component: ForumDetail
				},
				{
					path: 'myForumList',
					component: MyForumList
				},
				{
					path: 'storeup',
					component: Storeup
				},
				{
					path: 'news',
					component: News
				},
				{
					path: 'newsDetail',
					component: NewsDetail
				},
				{
					path: 'yonghu',
					component: yonghuList
				},
				{
					path: 'yonghuDetail',
					component: yonghuDetail
				},
				{
					path: 'yonghuAdd',
					component: yonghuAdd
				},
				{
					path: 'shipinbiaoqian',
					component: shipinbiaoqianList
				},
				{
					path: 'shipinbiaoqianDetail',
					component: shipinbiaoqianDetail
				},
				{
					path: 'shipinbiaoqianAdd',
					component: shipinbiaoqianAdd
				},
				{
					path: 'shipinfenlei',
					component: shipinfenleiList
				},
				{
					path: 'shipinfenleiDetail',
					component: shipinfenleiDetail
				},
				{
					path: 'shipinfenleiAdd',
					component: shipinfenleiAdd
				},
				{
					path: 'shipindianbo',
					component: shipindianboList
				},
				{
					path: 'shipindianboDetail',
					component: shipindianboDetail
				},
				{
					path: 'shipindianboAdd',
					component: shipindianboAdd
				},
				{
					path: 'newstype',
					component: newstypeList
				},
				{
					path: 'newstypeDetail',
					component: newstypeDetail
				},
				{
					path: 'newstypeAdd',
					component: newstypeAdd
				},
				{
					path: 'aboutus',
					component: aboutusList
				},
				{
					path: 'aboutusDetail',
					component: aboutusDetail
				},
				{
					path: 'aboutusAdd',
					component: aboutusAdd
				},
				{
					path: 'systemintro',
					component: systemintroList
				},
				{
					path: 'systemintroDetail',
					component: systemintroDetail
				},
				{
					path: 'systemintroAdd',
					component: systemintroAdd
				},
				{
					path: 'friendlink',
					component: friendlinkList
				},
				{
					path: 'friendlinkDetail',
					component: friendlinkDetail
				},
				{
					path: 'friendlinkAdd',
					component: friendlinkAdd
				},
				{
					path: 'onlinemessage',
					component: onlinemessageList
				},
				{
					path: 'onlinemessageDetail',
					component: onlinemessageDetail
				},
				{
					path: 'onlinemessageAdd',
					component: onlinemessageAdd
				},
				{
					path: 'discussshipindianbo',
					component: discussshipindianboList
				},
				{
					path: 'discussshipindianboDetail',
					component: discussshipindianboDetail
				},
				{
					path: 'discussshipindianboAdd',
					component: discussshipindianboAdd
				},
			]
		},
		{
			path: '/login',
			component: Login
		},
		{
			path: '/register',
			component: Register
		},
	]
})
