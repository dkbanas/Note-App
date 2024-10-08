PGDMP          	            |           NoteDB    16.3    16.3     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    49373    NoteDB    DATABASE     {   CREATE DATABASE "NoteDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Polish_Poland.1250';
    DROP DATABASE "NoteDB";
                postgres    false            �            1259    49380    Notes    TABLE     �   CREATE TABLE public."Notes" (
    "Id" integer NOT NULL,
    "Title" character varying(30) NOT NULL,
    "Description" text,
    "CreatedDate" bigint NOT NULL,
    "UserEmail" text NOT NULL
);
    DROP TABLE public."Notes";
       public         heap    postgres    false            �            1259    49379    Notes_Id_seq    SEQUENCE     �   CREATE SEQUENCE public."Notes_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public."Notes_Id_seq";
       public          postgres    false    217            �           0    0    Notes_Id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public."Notes_Id_seq" OWNED BY public."Notes"."Id";
          public          postgres    false    216            �            1259    49374    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap    postgres    false            T           2604    49383    Notes Id    DEFAULT     j   ALTER TABLE ONLY public."Notes" ALTER COLUMN "Id" SET DEFAULT nextval('public."Notes_Id_seq"'::regclass);
 ;   ALTER TABLE public."Notes" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    217    216    217            �          0    49380    Notes 
   TABLE DATA           [   COPY public."Notes" ("Id", "Title", "Description", "CreatedDate", "UserEmail") FROM stdin;
    public          postgres    false    217   �       �          0    49374    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public          postgres    false    215   [       �           0    0    Notes_Id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public."Notes_Id_seq"', 13, true);
          public          postgres    false    216            X           2606    49387    Notes PK_Notes 
   CONSTRAINT     R   ALTER TABLE ONLY public."Notes"
    ADD CONSTRAINT "PK_Notes" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Notes" DROP CONSTRAINT "PK_Notes";
       public            postgres    false    217            V           2606    49378 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public            postgres    false    215            �   �  x�mW�r�8>3Oч٪�J�Կ沱{<�g�qvR[�DB$"��d�O���s�G�'ٯA�ReU�r(������[Yr�lQH���ʇdHo�VK�h�t)��ǫZih��!�)����_���/�Ւv�!ڔ|���<�.�7i�ѯZm%Y��Í�rC{[u.$�(��G�x�-�g�H�q"�D�H�V5B��6���7Q�����q$�{�~�0K)�Xi�mh��N�j@5�N�k���
N��	MA
B���Fż�as�1�B.�3^����UR ѻ|j�u�~UZ�J������t��/�7��WR�������S����Vy�]�B��i>I���"��'�H�$�����ka �G�em�x�
e��(���@��7R�#>M5B�Y[�Z[��+j�%	2rG^�p[ �Kj��]�B\�������A�������+�M!V�!��;�*lg�t�����3*� 0O�,���3 ��V5�tc�F�:��'ȱ{e�3%���(Ӆۭ)�ǒV�������TUP�&)���IP��

;~�s��,��VJ����xC%B�#QR���9If2�gi:YL�3�L�w�{/\�ӝ4]2�C��I�8L��]{�43�V] �x�L�UJz�|��X0w�o�U���p-!L�_�H/����-�A�[4���mQ��
F��[薍_ҽ26�/V��m4�p'8�c����,�f�Q:����1M�l���
>w��6p!~��_���?I+&�3B��l��m�� P4Y �x���[z{ۧ�*�W�!bh�C�;�Bs hž��&�V�������l:-�s
6Kn�h�яbo;����p�O.c�r�y��Zz�!"�����>6^��G����A�s�{�qۈ`YZ��蚕�ftV��ʶ�	s�SX�۲��fJˁ�h[S1J�4�?���L�<O��sD�'�0�rqٕ.ᖃR-��`��B�/�r�l?d�<M����	2������t��8�<�6�<���G�@�4_>�S�@��H۱mƶ/Ᏻ,�-�����.�2���(�H���J[��F��V�u,��d�,��(������*��|���|�_X20e���l�~�Py�'~n�o�H5ⳖG�釖���DB�In���"��{������*r���f���	ݲ��u��--X�fߋSI�q��9
�w�b@Vϓ���J����0�N�ϳ|<���s}���H�� C�2���!(9���`��8@?�W�+��[?�0�g�O$F�Qf�;	JB�[t"���@R�7>��`��Y<���O�|>:��,Kn��Y���#L��FP��X_�Nj�P�ӣa�w}�81�W'x�@!{��Y-�����1�"Fp�/=���-�8�L��O˕�N!���y6��5Y�E�\� �J��� �(�{ D��k�&X�����`?�χ��r�T�H�&�?~��`�;�uI�ڌ��2k<�����/V�o-�c�Oír#��5������`&��|��潢�VFߣ����9}�M�y���1x���|ωV����[��z��֨7��55��7�G@L,^�&��n���Jx�G�2#,XQeYJ�Ϗ���棕Y�YJ���!�EL�pE��@�c\,��8 ����Eꊄ�>��:�V��h�z�Xk�
S��ϫҏ�2�v���"=�?8 ����	���q;�'0g��. yػ@aE��k43[����	nG����(��x:G�QrǳF�>���B���W6y7\����\�=��8�F�T�
91���e���a�5�V�磉n�%~7m`U���p�ۮğ MKo5r)'fw�x�Z�>�Tt�)sp9�K����Ū�e�.��DJC��?�j/6���`������M�{���V��.*#�!^	,��rj��w��|�Φ�lrVx/^�z�?�o��      �   *   x�32021�0440702�����,�L���3�3����� �2�     