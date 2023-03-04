{\rtf1\ansi\ansicpg1252\cocoartf2706
\cocoatextscaling0\cocoaplatform0{\fonttbl\f0\fnil\fcharset0 Menlo-Regular;}
{\colortbl;\red255\green255\blue255;\red0\green0\blue255;\red255\green255\blue254;\red25\green25\blue25;
\red101\green76\blue29;\red19\green120\blue72;}
{\*\expandedcolortbl;;\cssrgb\c0\c0\c100000;\cssrgb\c100000\c100000\c99608;\cssrgb\c12941\c12941\c12941;
\cssrgb\c47451\c36863\c14902;\cssrgb\c3529\c53333\c35294;}
\paperw11900\paperh16840\margl1440\margr1440\vieww11520\viewh8400\viewkind0
\deftab720
\pard\pardeftab720\partightenfactor0

\f0\fs24 \cf2 \cb3 \expnd0\expndtw0\kerning0
CREATE\cf4  \cf2 TABLE\cf4  [dbo].[consoles] (\cb1 \
\cb3     [id]      \cf2 INT\cf4            \cf5 IDENTITY\cf4  (\cf6 1\cf4 , \cf6 1\cf4 ) \cf2 NOT NULL\cf4 ,\cb1 \
\cb3     [console] \cf2 VARCHAR\cf4  (\cf6 50\cf4 )  \cf2 NULL\cf4 ,\cb1 \
\cb3     [logo]    \cf2 VARCHAR\cf4  (MAX) \cf2 NULL\cf4 ,\cb1 \
\cb3     \cf2 CONSTRAINT\cf4  [PK_consoles] \cf2 PRIMARY KEY\cf4  \cf2 CLUSTERED\cf4  ([id] \cf2 ASC\cf4 )\cb1 \
\cb3 );\cb1 \
\
\cf2 \cb3 CREATE\cf4  \cf2 TABLE\cf4  [dbo].[game] (\cb1 \
\cb3     [id]               \cf2 INT\cf4            \cf5 IDENTITY\cf4  (\cf6 1\cf4 , \cf6 1\cf4 ) \cf2 NOT NULL\cf4 ,\cb1 \
\cb3     [id_console]       \cf2 INT\cf4            \cf2 NULL\cf4 ,\cb1 \
\cb3     [name]             \cf2 VARCHAR\cf4  (MAX) \cf2 NULL\cf4 ,\cb1 \
\cb3     [descricao]        \cf2 VARCHAR\cf4  (MAX) \cf2 NULL\cf4 ,\cb1 \
\cb3     [data_compra]      \cf2 DATE\cf4           \cf2 NULL\cf4 ,\cb1 \
\cb3     [thumb]            \cf2 VARCHAR\cf4  (MAX) \cf2 NULL\cf4 ,\cb1 \
\cb3     [data_finalizacao] \cf2 DATE\cf4           \cf2 NULL\cf4 ,\cb1 \
\cb3     [finalizado]       \cf2 BIT\cf4            \cf2 NOT NULL\cf4 ,\cb1 \
\cb3     \cf2 CONSTRAINT\cf4  [PK_game] \cf2 PRIMARY KEY\cf4  \cf2 CLUSTERED\cf4  ([id] \cf2 ASC\cf4 ),\cb1 \
\cb3     \cf2 CONSTRAINT\cf4  [FK_game_consoles] \cf2 FOREIGN KEY\cf4  ([id_console]) \cf2 REFERENCES\cf4  [dbo].[consoles] ([id])\cb1 \
\cb3 );\cb1 \
\
}