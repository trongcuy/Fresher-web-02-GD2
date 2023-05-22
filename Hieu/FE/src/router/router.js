import { createRouter, createWebHistory } from 'vue-router'
import StoragePage from '@/components/view/page/StoragePage';
import HomePage from '@/components/view/HomePage/HomePage';
import DefaultPage from '@/components/view/page/Default/DefaultPage';
import MinePage from '@/components/view/page/Mine/MinePage';
import CreatePage from '@/components/view/page/Create/CreatePage';

const routes = [
  {
    path: '/storage',
    components: {
      homeview: StoragePage
    }
    ,
    children: [
      {
        path: '/storage',
        redirect: '/storage/mine'
      },
      {
        path: '/storage/mine',
        components: {
          mineview: MinePage
        }
      },
      {
        path: '/storage/school',
        components: {
          mineview: DefaultPage
        }
      },
      {
        path: '/storage/community',
        components: {
          mineview: DefaultPage
        }
      }
    ]
    ,
  },
  {
    path: '/dashboard',
    components: {
      homeview: HomePage
    }
  },
  {
    path: '/classroom',
    components: {
      homeview: HomePage
    }
  },
  {
    path: '/training',
    components: {
      homeview: HomePage
    }
  },
  {
    path: '/storage/create',
    components: {
      create: CreatePage
    }
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router