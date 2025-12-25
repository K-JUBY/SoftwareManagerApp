--
-- PostgreSQL database dump
--

\restrict QNdzAFVJVDfL4MFXHE2zuBMJ59SyQj89Yra2wH57Rgg0Q0MMlN5c5NeycbrnbuD

-- Dumped from database version 18.0
-- Dumped by pg_dump version 18.0

-- Started on 2025-12-25 21:32:39

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 234 (class 1255 OID 25171)
-- Name: find_software_by_category(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.find_software_by_category(category_name_query character varying) RETURNS TABLE(software_name character varying, developer_name character varying, website character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT
        s.name,
        d.developer_name,
        s.website
    FROM
        Software s
    JOIN
        Categories c ON s.category_id = c.category_id
    JOIN
        Developers d ON s.developer_id = d.developer_id
    WHERE
        c.category_name ILIKE '%' || category_name_query || '%'; -- ILIKE для поиска без учета регистра
END;
$$;


ALTER FUNCTION public.find_software_by_category(category_name_query character varying) OWNER TO postgres;

--
-- TOC entry 235 (class 1255 OID 25173)
-- Name: update_last_updated_at_column(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.update_last_updated_at_column() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
   NEW.last_updated_at = NOW(); -- Устанавливаем текущее время
   RETURN NEW;
END;
$$;


ALTER FUNCTION public.update_last_updated_at_column() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 220 (class 1259 OID 25069)
-- Name: categories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.categories (
    category_id integer NOT NULL,
    category_name character varying(100) NOT NULL
);


ALTER TABLE public.categories OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 25068)
-- Name: categories_category_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.categories_category_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.categories_category_id_seq OWNER TO postgres;

--
-- TOC entry 5001 (class 0 OID 0)
-- Dependencies: 219
-- Name: categories_category_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.categories_category_id_seq OWNED BY public.categories.category_id;


--
-- TOC entry 230 (class 1259 OID 25146)
-- Name: collections; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.collections (
    collection_id integer NOT NULL,
    collection_name character varying(150) NOT NULL,
    user_id integer
);


ALTER TABLE public.collections OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 25145)
-- Name: collections_collection_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.collections_collection_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.collections_collection_id_seq OWNER TO postgres;

--
-- TOC entry 5002 (class 0 OID 0)
-- Dependencies: 229
-- Name: collections_collection_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.collections_collection_id_seq OWNED BY public.collections.collection_id;


--
-- TOC entry 222 (class 1259 OID 25080)
-- Name: developers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.developers (
    developer_id integer NOT NULL,
    developer_name character varying(150) NOT NULL
);


ALTER TABLE public.developers OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 25079)
-- Name: developers_developer_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.developers_developer_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.developers_developer_id_seq OWNER TO postgres;

--
-- TOC entry 5003 (class 0 OID 0)
-- Dependencies: 221
-- Name: developers_developer_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.developers_developer_id_seq OWNED BY public.developers.developer_id;


--
-- TOC entry 228 (class 1259 OID 25128)
-- Name: reviews; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.reviews (
    review_id integer NOT NULL,
    software_id integer,
    author character varying(100) DEFAULT 'Аноним'::character varying,
    review_text text,
    rating integer,
    created_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT reviews_rating_check CHECK (((rating >= 1) AND (rating <= 5)))
);


ALTER TABLE public.reviews OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 25127)
-- Name: reviews_review_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.reviews_review_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.reviews_review_id_seq OWNER TO postgres;

--
-- TOC entry 5004 (class 0 OID 0)
-- Dependencies: 227
-- Name: reviews_review_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.reviews_review_id_seq OWNED BY public.reviews.review_id;


--
-- TOC entry 226 (class 1259 OID 25112)
-- Name: screenshots; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.screenshots (
    screenshot_id integer NOT NULL,
    software_id integer,
    image_data bytea NOT NULL,
    caption character varying(255)
);


ALTER TABLE public.screenshots OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 25111)
-- Name: screenshots_screenshot_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.screenshots_screenshot_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.screenshots_screenshot_id_seq OWNER TO postgres;

--
-- TOC entry 5005 (class 0 OID 0)
-- Dependencies: 225
-- Name: screenshots_screenshot_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.screenshots_screenshot_id_seq OWNED BY public.screenshots.screenshot_id;


--
-- TOC entry 224 (class 1259 OID 25091)
-- Name: software; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.software (
    software_id integer NOT NULL,
    name character varying(150) NOT NULL,
    description text,
    system_requirements text,
    size_mb numeric(10,2),
    website character varying(255),
    category_id integer,
    developer_id integer,
    last_updated_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    is_free boolean DEFAULT false NOT NULL
);


ALTER TABLE public.software OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 25154)
-- Name: software_collections; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.software_collections (
    software_id integer NOT NULL,
    collection_id integer NOT NULL
);


ALTER TABLE public.software_collections OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 25090)
-- Name: software_software_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.software_software_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.software_software_id_seq OWNER TO postgres;

--
-- TOC entry 5006 (class 0 OID 0)
-- Dependencies: 223
-- Name: software_software_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.software_software_id_seq OWNED BY public.software.software_id;


--
-- TOC entry 233 (class 1259 OID 25177)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    username character varying(100) NOT NULL,
    password_hash text NOT NULL,
    role character varying(50) NOT NULL,
    CONSTRAINT users_role_check CHECK (((role)::text = ANY ((ARRAY['Admin'::character varying, 'User'::character varying])::text[])))
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 25176)
-- Name: users_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.users_user_id_seq OWNER TO postgres;

--
-- TOC entry 5007 (class 0 OID 0)
-- Dependencies: 232
-- Name: users_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;


--
-- TOC entry 4791 (class 2604 OID 25072)
-- Name: categories category_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categories ALTER COLUMN category_id SET DEFAULT nextval('public.categories_category_id_seq'::regclass);


--
-- TOC entry 4800 (class 2604 OID 25149)
-- Name: collections collection_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.collections ALTER COLUMN collection_id SET DEFAULT nextval('public.collections_collection_id_seq'::regclass);


--
-- TOC entry 4792 (class 2604 OID 25083)
-- Name: developers developer_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.developers ALTER COLUMN developer_id SET DEFAULT nextval('public.developers_developer_id_seq'::regclass);


--
-- TOC entry 4797 (class 2604 OID 25131)
-- Name: reviews review_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reviews ALTER COLUMN review_id SET DEFAULT nextval('public.reviews_review_id_seq'::regclass);


--
-- TOC entry 4796 (class 2604 OID 25115)
-- Name: screenshots screenshot_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.screenshots ALTER COLUMN screenshot_id SET DEFAULT nextval('public.screenshots_screenshot_id_seq'::regclass);


--
-- TOC entry 4793 (class 2604 OID 25094)
-- Name: software software_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.software ALTER COLUMN software_id SET DEFAULT nextval('public.software_software_id_seq'::regclass);


--
-- TOC entry 4801 (class 2604 OID 25180)
-- Name: users user_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);


--
-- TOC entry 4982 (class 0 OID 25069)
-- Dependencies: 220
-- Data for Name: categories; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.categories (category_id, category_name) FROM stdin;
1	Текстовые редакторы
2	Графические редакторы
3	Антивирусы
4	Браузеры
5	Офисные пакеты
6	Медиаплееры
7	Архиваторы
8	Системные утилиты
9	Облачные хранилища
10	Мессенджеры
11	3D-моделирование
12	Аудиоредакторы
13	Запись видео
14	Корпоративные мессенджеры
15	Музыкальные сервисы
16	тест
\.


--
-- TOC entry 4992 (class 0 OID 25146)
-- Dependencies: 230
-- Data for Name: collections; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.collections (collection_id, collection_name, user_id) FROM stdin;
\.


--
-- TOC entry 4984 (class 0 OID 25080)
-- Dependencies: 222
-- Data for Name: developers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.developers (developer_id, developer_name) FROM stdin;
1	Microsoft
2	Adobe
3	Google
4	Mozilla Foundation
5	Kaspersky Lab
6	The Document Foundation
7	Sublime HQ
8	JetBrains
9	VideoLAN
10	7-Zip Team
11	win.rar GmbH
12	Piriform (CCleaner)
13	Dropbox, Inc.
14	Telegram FZ-LLC
15	Discord Inc.
16	Blender Foundation
17	Opera Software
18	Audacity Team
19	OBS Project
20	Slack Technologies
21	Spotify AB
22	Oracle
23	The GIMP Team
24	тест
\.


--
-- TOC entry 4990 (class 0 OID 25128)
-- Dependencies: 228
-- Data for Name: reviews; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.reviews (review_id, software_id, author, review_text, rating, created_at) FROM stdin;
1	1	Иван Петров	Отличный редактор, пользуюсь каждый день!	5	2025-12-22 19:37:43.938139+03
2	1	Анна Сидорова	Много плагинов, очень удобно.	5	2025-12-22 19:37:43.938139+03
3	4	Сергей	Незаменимый инструмент для фотографа.	5	2025-12-22 19:37:43.938139+03
4	5	Елена	Для бесплантой программы - просто супер!	4	2025-12-22 19:37:43.938139+03
5	9	Дмитрий	Самый быстрый браузер.	4	2025-12-22 19:37:43.938139+03
6	12	Ольга	Полностью заменил мне платный офисный пакет.	5	2025-12-22 19:37:43.938139+03
7	8	user	такое себе приложение	2	2025-12-24 20:29:59.892029+03
\.


--
-- TOC entry 4988 (class 0 OID 25112)
-- Dependencies: 226
-- Data for Name: screenshots; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.screenshots (screenshot_id, software_id, image_data, caption) FROM stdin;
1	4	\\x52494646ae0f000057454250565038580a00000008000000670100670100565038200a0f0000305e009d012a680168013e753a9a48a4a3a2a123d4da18900e894ddc2e622ca62c63ec263fc1ddc2e33fb9ff69fdb4fed9efa354fea5fd53f55ff6afa18c847fcfc67f63ffb7fe1fdb7ffaef52dfa73d803f4dff64bac77980fdc8f5b3ff21fa81ee8ffd0fa807f61ff9bd647e81ffb49e9cdecb3fb87fb9ded93d401ffffac3fdb3fb776a1b4eb7eba0d28bbb0594c6003c016665f8db06c7a0eff29514e9ea72e2f404a640ec8a74f1bb9420442705cfa446367f46cd14595de1e1d03f3776e2132fa297a0250894cd870660f0f11f22fcdbcb4f066e668a2caef0f2a42331d421b4c4bfe2558e12c421bbd4bb5ce21fc7ce91ff62b3c738bd0129903a401954873bf5aa11ed3914882d35271009693729b2a74f539717a0017d4ed8aad753295a7e84369ea72e2f4013dce26f6300211b0b1828fb5d74625b81afd8d77325eac09025f9844c3b229d340bf67c7d757b84cf4865de54df941bbc0562d14d1215449beec2377e8027b9c4dec6004234e871a966ca995c24521dab613bc649015b2d7c6fa94c8bfa3edf8c1e6ed94871bc75741b6400d9cc7fe6af2c82653e945951904a3fb8420b1dda0af7dab416a2f53646ade7ecd1f034e9f17ff2a2ccb8da272a53955ba85620826dc21ecc33a517a23af6996e0bb19e83b5141fbe42be6d16b7c41b3468b9f19a666567fa55734b319987aa9cecae144df0b31c7bb5be20d9a1ecad41563b67adf6054124197ab080773c2f0aa15e0831635663f637242259d3e2ffe5459971f22bd74f898a44564727c5ffca8b32e3d74c81d914e97620dde929f8ca569fa10da7a9cb8bceb9539636d548013adf1eaced4fa3e75eb0d9cb16f6163051f6a5960f81bad0e0b182ea9fc3f2e2f3b072fe3e55a1bbdc29b41eda7a9b1dae6de99a53356023cb03bb207645b0161258ac78a4dd8efddf1a22f404a5f11bc696a6743cf4976571c46b914e977608d740ecdd14c31a1a8197dae8955abfa3668a269008c7454d166c2581eb8bd012990804cd7ede5d480733d1385165778779b7f20542e453a7a9cb8bd0129903b229d3c60000feff56c00001013597842d872b025c9ec0eec33cfb0d79654c06bc11570f5baf8b24b922efa6ce49beef9a9112d68343e83b8c0c7f57167bc13b41deff174c72c84a5b816287f853c63bce14f7412ade25a53819d2a1b90dc99200f2863231489c4ebf8f34cef97d2946f5376a75088ca715f81ed00dab288e2a1274ff859cb6c61a0b864cd898d679a60a86130da43af3fc425c4e2930c25f8cffd50d3f4cbcd098b7dbe42b8191a271f044987815ef466c180c051c144a7e89cd105df340f0d8855eda1f1187056a7c08d5fc4e3363bc378e3c5b4862dc49ebdb64020a37c56fbf21304d523863f9d827a879c5040352a0296110b25391a359cfd3f16c842407bb7f96be1ffb04d371c73f50d8daaa7d23f528b355d22aa54457774222b3f4552da7fba69db6fe234ebfeb345631fdd325b640aa9fc84b8c0ecd2474c1344a931273b02188da8e73d284184543c64b7e81a6087d2b9ea8fc0427664518580a35991f049e3a918708b17d33606c1291475dfeb68ccf275039313b260a48830e70e958fb16b2700472d474813f3ad736d443568a51282652fb1d2191f0518f56acf8336e9fde404947f85fa1548a00fcf41134e24117060d8bb2dd9e7977d701804d6abd70fde07e7b7cdcac1d640227f685e42a436f697444c604e54d39f3b3459775914ebbeb5b77078c72c0e9cd6cd090399a40cb28f198a55e147fabe1465bd77b032afde2db20c55ad54e43b3687df0e2f84bc9ee22a340c4ba3eeea6cf0014001342116b4341c1e9f1908728465d85078370d3e81580fd38b99f77043746d1ca3f0d874421c3a27849217989d6c6d241db99c506b7b49e863a48be6bec8acc69043709bb1bff943b4fb06919b975a8539be1e918022b45762ff1c49486dd61787a888edc150afebc0dfc437d63cc436d5362c6d73678a0a80a5c8fe1e11e4427320444b22104af14ff898bec800cdb71fa332acc6e962c8d184566913632a1d11be8eb482fea2435b4f39d835e2841f93c3798b680be3b571bb18da51de605ee2909c066a30d27e7a776a2a4d0f3b2e85dedecfa067781ba73ec81167a934c853e9ee24754cc75e32013215976bb30049a868856fc5a00c12c4f8f46647f11410d1f97321d5fe8521849701e2c074c6db2c5a24904dae235236c370ec8d243a2f26c76816ba86fad20e9b20b35b70a2dae9b953b77b7ae9898c58ca84db0f522e46f06a7c57fb5797835417f2a2f77be1ee58ca6f273dc65f7ba90f01416b60e7a899c2046b1a2e2a557f9691beb6f82c7fcc038f0685077190058743eafca9b08677c72d61933d207a912039956a1be6341e3c7d08435c507d3dd3e751631d6d137f0c30ec3121846cea8a0514e05c058c8090b0028fe82e83b3cc96acd11ccbf7843eda86f3770f74877842f7cb0c0fa2b62ef8db74e20f5b490f4e570359ff9116ad3ca0a707cf5515853943648ec618dea4431e478f49754269353eff590f71e3e1f6289585d05aad34e4ebb07197185fbdacb83b826371376d63da91995514100b89cbf9d7fd84ba76190cb3505b573248a0864f70783f374f68196c6145a7c468d863c46dd1aefd19257aa3531678fb4453d00c3db740b1f859fcee3b041d1d64fc2ce4d0f796d6b4e732c666832972699270971bb2be2fd6ba685eddff10e30b62e8cba1f8d73d174b25d5435ec009ed638d8fd2fa38b190e4e023dd061362686caaf08a36d93c0f6d025e24e6802dedba62903d4675b264b4b0592ce9cdaee60d3c8861d18140103b964c2ccf6fea697fcea5fb1e7a3322a91a6bc16c0dbae78ab729f6317684f66d691c138ff15bc24dc5894fc3478085f2a072b45f1ef61315f854272dd17c6f084ef44b2493c87ff6037c6eb9c8be73a1d5a9b554b2d3982df2cf7ecb93674f274c5a0e4296709b69128d5845f8b7a0536ab9a700cc0aedddd44c2d1cb5fed50b44432387c37d058533ae48df62ee5d71a3195b7c13f5ec43f3a89d31bb15eaee4afa399b3fb354f6c2a38fc1a8e8bce6b90d2229303e4bb04fa8a57d0e4920eeea45978bd589751f43da9263b38079a0c8cd8b0c02cb31428fe8cb89b5a8063c80190fc397f034158c9da090b7f7f078536767086551798339bda0dbf81aec96afe13fd050a9c9eded5d4608b12a1247198c6c362a97e84d7885cc1efa04fc594a1487f34703645bb83af7a581cedee1ad5bd6d0fc1169bc165c7b06dca9df9ca46d3726b4e8e40b23b4cf32a68bf22be2a26d739de89223c6f057dade939fd45bb680ae949f224b62e62af2618ebd858dc7dff5a01ed3c5677f3e5b2b963b08e97917a07e50fb18774ddd8a72847eec77f600fa2bd6388e5b5fba103d5955c479dff79437940a9e297168f8984b2c265eba3f1bb7f9ce169e8061edbc9553452fa99ef4f007c760a4310cfa05ecc04abcd4c1e79c2f0800441c6413eff30c8d8efeaf83d81ab6874e3935a5f352b287cb4f18ffcca7b9da57d06aee3897c06e9cd2b4c2d19224d53d0de57bb01b0943679cf26ec949c643800bf7d5104e71399f58fefc3e2c5ac64826ec3dd43c32a6ae4a3c6f4ab80627a3d185849c40bb62f922a1145b3c5f376bac9fcfcd63ee71febd614a1c277e8179dd47470512e88e722ddbaeae496748c897918f5cb22e1636e4deae5c68c136f10246373825bc91feb3f8a229ccb378a2792f2baadf2debabddac49edbe89b8b80e616ce4b9ce4ba06397e3d16b5c134d7ce3b8aaa41552af471b5ac76fb2fc8d47cb39382cf98d3315acba9033fbb8beb9d64981af8c8c2c90e0b1a7ad63bf44563dcafab4b233814d5892e85105814b31164b02d44b35a3bc3611dea6bb8804cdc73c6135a11ea1d46604d1960822688e965ff4e20633676c8e426cccb8276c785d7cd3fb644e4ac5435dc35a2680abf1afc4baa62920d3bf40bcee50071d37feda23a84894efd02f3b940e14b4c5987f0b4d4306f7f655f618bba819e9ce4bf7522ca83f552ee4c72852708d95dcde6f290891ddcbfde616e491f3bc3a80f0cb111416cc9f4da9066329b933483d468fd4ddf863926178d1d072892caefe127f2c689b7830061396eca5f18bd389bf8abd2d3b6bf20df0ff0a8c0b30f57abb88e20c67bb90a2aaf75d20c15b26dcab55d3f8cf20ff8b6daf2baf03290f5d1ce0b46d853f9d60c85d0b388a7eb06982ef3df17668578c30b328d986aeef1c27fc33edb921e2cfe0089360aa59446f112e27744f47cf6c61794d47699aca942102cdc107095e8676af759116445fc29b2ae0d97c91408aae3e6882cf9f66247429e29e74c9ff0938639e145bd7a3b149fa4c51bdd106bc0e4ddef3e2e31335e44c03af8171403c8bf7129ea07eaa0941988800a8162df64b2f8853cf3dae95e041c3ed2feff7c6c5eefcd2026f5e5160dfb1c84ab3fc6ea5daee2db417e666a74749f0ac0215ef36eb4f180c1eefc52753ff562ceb427abbbd1400a10a292d4ba15ba6fe3c487f809906bc126de0573028d8bff317280c3ede1109f31754f537ee172dedf95f1095f31835f76198afd48f0bd4252e6b3e706a1a8f7cf4e19a2194d1caaf4e68d8277cc4e02bd5651739c5175c0ef4a6716b5b006c32fd758333f0bf0bb796f409165099359b3d268050d429b0bfaddb85722c21615ab3f9b44c80fdf440005046ee18f1a901de17ace545e22d9a422829a01de0be4e4a6adb1a1003f3507f62589eb44bc4403e1e9d6e8c37e8ff97eb66888b7a86303c5863dfe16259629d0cd5ccb90e96f1e247221292f075bbcc91ef5fcf1f7e3d4a671462064e613ac1fe52cf3c8e89ec803904013ba3a13f1ca8801020e27b91bd938778374d38598b1c596cb414b0d1e75cf831d1c6fb4b0cc8d70bfc3d7a3502fdf4288444de6a5334af69111d43760719b93fe53550907a3286d0d7af8b04375b8b896da31f7ed266b3132368484a0224446d2114694cc3dedf26860faaba049e950b9e1ff9ae92f5385bbc67ffdce480005d4e6834940298f17be4583df31467205912714e659cc508f17a7488779314341b21e087670896fd6676e022e42296d20177fe9343bd4f3cfc16989de3cb9c50de48fc44dd62abca56ac1dfaf4dfd2b356993a91c5b767fab9363c15463e37ccdec1105a296293383e773c317f14b98b8317062e0c5c18d42f93567092cdeddc782a2ec904b50c501c83245d46b98048cfdf83ee58fd1e8ffc1799bad235da1f9427412cec12e82550f7e428a8dd9985c142efddfe0b99f02f4776322000013c17f8f87df0519bc352f82a005117b98f92643e4927252c2db06de2013011eac77b8f1c39eb112d2a51a1af3ef0875f57800000000000000455849467e0000004578696600004d4d002a000000080005011200030000000100010000011a0005000000010000004a011b0005000000010000005201280003000000010002000087690004000000010000005a00000000000000480000000100000048000000010002a00200040000000100000168a0030004000000010000016800000000	\N
\.


--
-- TOC entry 4986 (class 0 OID 25091)
-- Dependencies: 224
-- Data for Name: software; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.software (software_id, name, description, system_requirements, size_mb, website, category_id, developer_id, last_updated_at, is_free) FROM stdin;
2	Sublime Text	Быстрый и многофункциональный текстовый редактор.	Windows, macOS, Linux	20.00	https://www.sublimetext.com/	1	7	2025-12-22 19:39:35.845138+03	f
7	Kaspersky Internet Security	Комплексное антивирусное решение.	Windows, macOS, Android	150.00	https://www.kaspersky.ru/	3	5	2025-12-22 19:39:35.845138+03	f
9	Google Chrome	Популярный веб-браузер от Google.	Windows, macOS, Linux, Android, iOS	100.00	https://www.google.com/chrome/	4	3	2025-12-22 19:39:35.845138+03	f
10	Mozilla Firefox	Свободный веб-браузер на движке Gecko.	Windows, macOS, Linux	55.00	https://www.mozilla.org/firefox/	4	4	2025-12-22 19:39:35.845138+03	f
11	Microsoft Office	Набор офисных приложений.	Windows, macOS	4000.00	https://www.office.com/	5	1	2025-12-22 19:39:35.845138+03	f
13	PyCharm	IDE для разработки на Python.	Windows, macOS, Linux	400.00	https://www.jetbrains.com/pycharm/	1	8	2025-12-22 19:39:35.845138+03	f
14	IntelliJ IDEA	IDE для Java-разработки.	Windows, macOS, Linux	800.00	https://www.jetbrains.com/idea/	1	8	2025-12-22 19:39:35.845138+03	f
8	Dr.Web Security Space1	Антивирусная защита от компании «Доктор Веб».	Windows, macOS, Linux	600.00	https://www.drweb.ru/	3	5	2025-12-22 20:14:14.860666+03	f
4	Adobe Photoshop	Растровый 	Windows 10/11, macOS	2000.00	https://www.adobe.com/products/photoshop.html	2	2	2025-12-23 19:16:49.925408+03	f
1	Visual Studio Code	Редактор кода от Microsoft.	Windows 10/11, macOS, Linux	80.00	https://code.visualstudio.com/	1	1	2025-12-23 20:44:29.767062+03	t
3	Notepad++	Бесплатный редактор текстовых файлов с подсветкой синтаксиса.	Windows	4.00	https://notepad-plus-plus.org/	1	1	2025-12-23 20:44:29.767062+03	t
5	GIMP	Бесплатный аналог Photoshop.	Windows, macOS, Linux	250.00	https://www.gimp.org/	2	2	2025-12-23 20:44:29.767062+03	t
6	Paint.NET	Простой, но мощный графический редактор для Windows.	Windows 7/8/10/11	15.00	https://www.getpaint.net/	2	2	2025-12-23 20:44:29.767062+03	t
12	LibreOffice	Свободный и бесплатный офисный пакет.	Windows, macOS, Linux	300.00	https://www.libreoffice.org/	5	6	2025-12-23 20:44:29.767062+03	t
15	VLC Media Player	Свободный кроссплатформенный медиаплеер, который воспроизводит большинство мультимедийных файлов.	Windows, macOS, Linux, Android, iOS	40.00	https://www.videolan.org/vlc/	6	9	2025-12-24 20:52:55.541261+03	t
16	AIMP	Бесплатный аудиопроигрыватель с большим набором функций и поддержкой множества форматов.	Windows, Android	12.00	https://www.aimp.ru/	6	\N	2025-12-24 20:52:55.541261+03	t
17	Winamp	Классический медиаплеер для Windows с поддержкой скинов и плагинов.	Windows	8.00	https://www.winamp.com/	6	\N	2025-12-24 20:52:55.541261+03	t
18	foobar2000	Продвинутый аудиоплеер для Windows с минималистичным интерфейсом и широкими возможностями кастомизации.	Windows	5.00	https://www.foobar2000.org/	6	\N	2025-12-24 20:52:55.541261+03	t
19	7-Zip	Бесплатный файловый архиватор с высокой степенью сжатия.	Windows, Linux	1.50	https://www.7-zip.org/	7	10	2025-12-24 20:52:55.541261+03	t
20	WinRAR	Мощный условно-бесплатный архиватор для Windows, поддерживающий формат RAR.	Windows	3.00	https://www.win-rar.com/	7	11	2025-12-24 20:52:55.541261+03	f
21	PeaZip	Бесплатный архиватор с открытым исходным кодом, поддерживающий более 180 форматов.	Windows, Linux, macOS	10.00	https://peazip.github.io/	7	\N	2025-12-24 20:52:55.541261+03	t
22	CCleaner	Утилита для очистки системы от временных файлов и оптимизации работы Windows.	Windows, macOS	35.00	https://www.ccleaner.com/	8	12	2025-12-24 20:52:55.541261+03	t
23	Speccy	Бесплатная утилита для получения подробной информации об аппаратном обеспечении компьютера.	Windows	9.00	https://www.ccleaner.com/speccy	8	12	2025-12-24 20:52:55.541261+03	t
24	Total Commander	Популярный двухпанельный файловый менеджер для Windows.	Windows	4.00	https://www.ghisler.com/	8	\N	2025-12-24 20:52:55.541261+03	f
25	Dropbox	Сервис облачного хранения данных, позволяющий пользователям хранить свои файлы на удаленных серверах.	Windows, macOS, Linux, Mobile	150.00	https://www.dropbox.com/	9	13	2025-12-24 20:52:55.541261+03	t
26	Google Drive	Облачное хранилище от Google, тесно интегрированное с другими сервисами компании.	Web, Windows, macOS, Mobile	1.00	https://www.google.com/drive/	9	3	2025-12-24 20:52:55.541261+03	t
27	Яндекс.Диск	Облачный сервис от Яндекса для хранения файлов и обмена ими.	Web, Windows, macOS, Mobile	1.00	https://disk.yandex.ru/	9	\N	2025-12-24 20:52:55.541261+03	t
28	Telegram	Кроссплатформенный мессенджер с акцентом на скорость и безопасность.	Windows, macOS, Linux, Mobile	40.00	https://telegram.org/	10	14	2025-12-24 20:52:55.541261+03	t
29	Discord	Платформа для общения голосом, видео и текстом, популярная среди геймеров.	Windows, macOS, Linux, Mobile	80.00	https://discord.com/	10	15	2025-12-24 20:52:55.541261+03	t
30	Skype	Программа для видео- и голосовых звонков через интернет.	Windows, macOS, Linux, Mobile	70.00	https://www.skype.com/	10	1	2025-12-24 20:52:55.541261+03	t
31	Slack	Корпоративный мессенджер для общения и совместной работы.	Windows, macOS, Linux, Mobile	100.00	https://slack.com/	14	20	2025-12-24 20:52:55.541261+03	f
32	Blender	Мощный бесплатный пакет для создания трёхмерной компьютерной графики.	Windows, macOS, Linux	200.00	https://www.blender.org/	11	16	2025-12-24 20:52:55.541261+03	t
33	Audacity	Бесплатный, простой в использовании многодорожечный аудиоредактор и рекордер.	Windows, macOS, Linux	30.00	https://www.audacityteam.org/	12	18	2025-12-24 20:52:55.541261+03	t
34	FL Studio	Профессиональная цифровая звуковая рабочая станция (DAW) для создания музыки.	Windows, macOS	1000.00	https://www.image-line.com/	12	\N	2025-12-24 20:52:55.541261+03	f
35	Opera	Веб-браузер со встроенным VPN и блокировщиком рекламы.	Windows, macOS, Linux	90.00	https://www.opera.com/	4	17	2025-12-24 20:52:55.541261+03	t
36	Microsoft Edge	Веб-браузер от Microsoft на движке Chromium.	Windows, macOS, Mobile	120.00	https://www.microsoft.com/edge	4	1	2025-12-24 20:52:55.541261+03	t
37	Atom	Бесплатный текстовый редактор с открытым исходным кодом от GitHub.	Windows, macOS, Linux	180.00	https://atom.io/	1	\N	2025-12-24 20:52:55.541261+03	t
38	Krita	Профессиональный бесплатный редактор для цифровой живописи.	Windows, macOS, Linux	120.00	https://krita.org/	2	\N	2025-12-24 20:52:55.541261+03	t
39	Inkscape	Мощный бесплатный редактор векторной графики.	Windows, macOS, Linux	90.00	https://inkscape.org/	2	\N	2025-12-24 20:52:55.541261+03	t
40	Avast Free Antivirus	Бесплатный антивирус для домашнего использования.	Windows, macOS	200.00	https://www.avast.com/	3	\N	2025-12-24 20:52:55.541261+03	t
41	ESET NOD32 Antivirus	Коммерческий антивирус с высокой скоростью работы.	Windows	100.00	https://www.eset.com/	3	\N	2025-12-24 20:52:55.541261+03	f
42	WPS Office	Бесплатный офисный пакет, совместимый с форматами Microsoft Office.	Windows, Linux, Mobile	150.00	https://www.wps.com/	5	\N	2025-12-24 20:52:55.541261+03	t
43	OnlyOffice	Онлайн-офис для совместной работы с документами.	Web, Windows, Linux, macOS	300.00	https://www.onlyoffice.com/	5	\N	2025-12-24 20:52:55.541261+03	t
44	OBS Studio	Бесплатная программа для записи видео и потокового вещания.	Windows, macOS, Linux	110.00	https://obsproject.com/	13	19	2025-12-24 20:52:55.541261+03	t
45	Spotify	Сервис потокового аудио, позволяющий легально прослушивать музыку.	Windows, macOS, Mobile	100.00	https://www.spotify.com/	15	21	2025-12-24 20:52:55.541261+03	t
46	VirtualBox	Программный продукт виртуализации для операционных систем.	Windows, macOS, Linux	103.00	https://www.virtualbox.org/	8	22	2025-12-24 20:52:55.541261+03	t
47	TeamViewer	Программа для удаленного управления компьютерами.	Windows, macOS, Linux, Mobile	30.00	https://www.teamviewer.com/	8	\N	2025-12-24 20:52:55.541261+03	t
48	qBittorrent	Свободный кроссплатформенный BitTorrent-клиент.	Windows, macOS, Linux	25.00	https://www.qbittorrent.org/	8	\N	2025-12-24 20:52:55.541261+03	t
49	Viber	Приложение для звонков и обмена сообщениями.	Windows, macOS, Mobile	120.00	https://www.viber.com/	10	\N	2025-12-24 20:52:55.541261+03	t
50	WhatsApp	Популярный мессенджер для смартфонов.	Windows, macOS, Mobile	130.00	https://www.whatsapp.com/	10	\N	2025-12-24 20:52:55.541261+03	t
51	DaVinci Resolve	Профессиональная система для монтажа и цветокоррекции видео.	Windows, macOS, Linux	2000.00	https://www.blackmagicdesign.com/products/davinciresolve	\N	\N	2025-12-24 20:52:55.541261+03	t
52	Zoom	Сервис для проведения видеоконференций и онлайн-встреч.	Windows, macOS, Mobile	20.00	https://zoom.us/	14	\N	2025-12-24 20:52:55.541261+03	t
53	Figma	Онлайн-сервис для разработки интерфейсов и прототипирования.	Web, Windows, macOS	5.00	https://www.figma.com/	2	\N	2025-12-24 20:52:55.541261+03	t
54	Notion	Приложение для организации заметок, задач, баз данных.	Web, Windows, macOS, Mobile	10.00	https://www.notion.so/	5	\N	2025-12-24 20:52:55.541261+03	t
55	тест			0.00		16	24	2025-12-25 19:57:28.75562+03	f
\.


--
-- TOC entry 4993 (class 0 OID 25154)
-- Dependencies: 231
-- Data for Name: software_collections; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.software_collections (software_id, collection_id) FROM stdin;
\.


--
-- TOC entry 4995 (class 0 OID 25177)
-- Dependencies: 233
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (user_id, username, password_hash, role) FROM stdin;
1	admin	$2a$11$nEbm7GPHaOmmjuXaEGJ0je.GRG.CaqGzHkF.UuLAfSnm8.KAYfuYC	Admin
2	user	$2a$11$mQPVjhH.QhuiPQRQo/RugeqhVX7vwU5Q3ao.Im7FcfNDY2IhQCeP2	User
\.


--
-- TOC entry 5008 (class 0 OID 0)
-- Dependencies: 219
-- Name: categories_category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.categories_category_id_seq', 16, true);


--
-- TOC entry 5009 (class 0 OID 0)
-- Dependencies: 229
-- Name: collections_collection_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.collections_collection_id_seq', 1, false);


--
-- TOC entry 5010 (class 0 OID 0)
-- Dependencies: 221
-- Name: developers_developer_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.developers_developer_id_seq', 24, true);


--
-- TOC entry 5011 (class 0 OID 0)
-- Dependencies: 227
-- Name: reviews_review_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.reviews_review_id_seq', 7, true);


--
-- TOC entry 5012 (class 0 OID 0)
-- Dependencies: 225
-- Name: screenshots_screenshot_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.screenshots_screenshot_id_seq', 1, true);


--
-- TOC entry 5013 (class 0 OID 0)
-- Dependencies: 223
-- Name: software_software_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.software_software_id_seq', 55, true);


--
-- TOC entry 5014 (class 0 OID 0)
-- Dependencies: 232
-- Name: users_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_user_id_seq', 2, true);


--
-- TOC entry 4805 (class 2606 OID 25078)
-- Name: categories categories_category_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_category_name_key UNIQUE (category_name);


--
-- TOC entry 4807 (class 2606 OID 25076)
-- Name: categories categories_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (category_id);


--
-- TOC entry 4819 (class 2606 OID 25153)
-- Name: collections collections_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.collections
    ADD CONSTRAINT collections_pkey PRIMARY KEY (collection_id);


--
-- TOC entry 4809 (class 2606 OID 25089)
-- Name: developers developers_developer_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.developers
    ADD CONSTRAINT developers_developer_name_key UNIQUE (developer_name);


--
-- TOC entry 4811 (class 2606 OID 25087)
-- Name: developers developers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.developers
    ADD CONSTRAINT developers_pkey PRIMARY KEY (developer_id);


--
-- TOC entry 4817 (class 2606 OID 25139)
-- Name: reviews reviews_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reviews
    ADD CONSTRAINT reviews_pkey PRIMARY KEY (review_id);


--
-- TOC entry 4815 (class 2606 OID 25121)
-- Name: screenshots screenshots_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.screenshots
    ADD CONSTRAINT screenshots_pkey PRIMARY KEY (screenshot_id);


--
-- TOC entry 4821 (class 2606 OID 25160)
-- Name: software_collections software_collections_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.software_collections
    ADD CONSTRAINT software_collections_pkey PRIMARY KEY (software_id, collection_id);


--
-- TOC entry 4813 (class 2606 OID 25100)
-- Name: software software_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.software
    ADD CONSTRAINT software_pkey PRIMARY KEY (software_id);


--
-- TOC entry 4823 (class 2606 OID 25189)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- TOC entry 4825 (class 2606 OID 25191)
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- TOC entry 4833 (class 2620 OID 25174)
-- Name: software update_software_modtime; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER update_software_modtime BEFORE UPDATE ON public.software FOR EACH ROW EXECUTE FUNCTION public.update_last_updated_at_column();


--
-- TOC entry 4826 (class 2606 OID 25101)
-- Name: software fk_category; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.software
    ADD CONSTRAINT fk_category FOREIGN KEY (category_id) REFERENCES public.categories(category_id);


--
-- TOC entry 4831 (class 2606 OID 25166)
-- Name: software_collections fk_collection; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.software_collections
    ADD CONSTRAINT fk_collection FOREIGN KEY (collection_id) REFERENCES public.collections(collection_id) ON DELETE CASCADE;


--
-- TOC entry 4827 (class 2606 OID 25106)
-- Name: software fk_developer; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.software
    ADD CONSTRAINT fk_developer FOREIGN KEY (developer_id) REFERENCES public.developers(developer_id);


--
-- TOC entry 4832 (class 2606 OID 25161)
-- Name: software_collections fk_software; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.software_collections
    ADD CONSTRAINT fk_software FOREIGN KEY (software_id) REFERENCES public.software(software_id) ON DELETE CASCADE;


--
-- TOC entry 4829 (class 2606 OID 25140)
-- Name: reviews fk_software_reviews; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reviews
    ADD CONSTRAINT fk_software_reviews FOREIGN KEY (software_id) REFERENCES public.software(software_id) ON DELETE CASCADE;


--
-- TOC entry 4828 (class 2606 OID 25122)
-- Name: screenshots fk_software_screenshots; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.screenshots
    ADD CONSTRAINT fk_software_screenshots FOREIGN KEY (software_id) REFERENCES public.software(software_id) ON DELETE CASCADE;


--
-- TOC entry 4830 (class 2606 OID 25192)
-- Name: collections fk_user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.collections
    ADD CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON DELETE CASCADE;


-- Completed on 2025-12-25 21:32:40

--
-- PostgreSQL database dump complete
--

\unrestrict QNdzAFVJVDfL4MFXHE2zuBMJ59SyQj89Yra2wH57Rgg0Q0MMlN5c5NeycbrnbuD

